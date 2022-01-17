using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UCNLDrivers;
using UCNLNav;
using UCNLNMEA;

namespace UGPSHub
{
    public enum PortType : int
    {
        RedNODE = 0,
        AUX = 1,
        OUT = 2,
        EMU_AUX = 3,
        EMU_RN = 4,
        INVALID
    }

    public enum PortState
    {
        NOT_USED,
        CLOSED,
        OPEN,
        TIMEOUT,
        ERROR,
        OK,
        UNKNOWN,
        INVALID
    }

    public enum TBAEnum
    {
        GOOD,
        FAIR,
        OUT_OF_BASE,
        INVALID
    }

    public class BaseState
    {
        #region Properties

        public RedBASEStatus Base1Status;
        public RedBASEStatus Base2Status;
        public RedBASEStatus Base3Status;
        public RedBASEStatus Base4Status;

        public double Base1MSR;
        public double Base2MSR;
        public double Base3MSR;
        public double Base4MSR;

        #endregion

        #region Constructor

        public BaseState(RedBASEStatus rb1sts, double rb1msr, RedBASEStatus rb2sts, double rb2msr,
            RedBASEStatus rb3sts, double rb3msr, RedBASEStatus rb4sts, double rb4msr)
        {
            Base1Status = rb1sts;
            Base2Status = rb2sts;
            Base3Status = rb3sts;
            Base4Status = rb4sts;

            Base1MSR = rb1msr;
            Base2MSR = rb2msr;
            Base3MSR = rb3msr;
            Base4MSR = rb4msr;
        }

        #endregion
    }

    #region Custom EventArgs

    public class TrackUpdateEventArgs : EventArgs
    {
        #region Properties

        public string TrackID { get; private set; }
        public GeoPoint3DETm TrackPoint { get; private set; }
        public double Course_deg { get; private set; }
        public double Speed_mps { get; private set; }

        #endregion

        #region Constructor

        public TrackUpdateEventArgs(string trackID, GeoPoint3DETm trackPoint)
            : this(trackID, trackPoint, double.NaN, double.NaN)
        {
        }

        public TrackUpdateEventArgs(string trackID, GeoPoint3DETm trackPoint, double course_deg)
            : this(trackID, trackPoint, course_deg, double.NaN)
        {
        }

        public TrackUpdateEventArgs(string trackID, GeoPoint3DETm trackPoint, double course_deg, double speed_mps)
        {
            TrackID = trackID;
            TrackPoint = trackPoint;
            Course_deg = course_deg;
            Speed_mps = speed_mps;
        }

        #endregion
    }

    public class PortTimeoutEventArgs : EventArgs
    {
        #region Properties

        public string PortName { get; private set; }
        public string PortDescr { get; private set; }

        #endregion

        #region Constructor

        public PortTimeoutEventArgs(string portName, string portDescr)
        {
            PortName = portName;
            PortDescr = portDescr;
        }

        #endregion
    }

    #endregion

    public class CCore : IDisposable
    {
        #region Properties

        char[] charSeps = new char[] { ' ' };
        char[] trimChars = new char[] { '(', ')' };

        bool disposed = false;

        readonly string emuPortNameAUX = "EMU_AUX";
        readonly string emuPortNameRDN = "EMU_RDN";
        int emuPortAUXID, emuPortRDNID;

        readonly int timerPeriodMS = 100;
        readonly int portsTimeoutMS = 3000;
        readonly int systemUpdateTimeoutMS = 1000;
        int redNodePortTS = 0;
        int auxPortTS = 0;
        int systemUpdateTS = 0;

        NMEASerialPort redNodePort;
        NMEASerialPort auxPort;
        NMEASerialPort outPort;
        NMEAMultipleListener listener;

        PrecisionTimer timer;

        Dictionary<int, string> portNameByHash;
        Dictionary<string, int> portHashByName;
        Dictionary<int, PortType> portTypeByHash;

        CourseEstimatorLA2D crsEstimator;
        TrackFilter trkSmoother;

        double radialErrorThreshold = 25.0;
        public double RadialErrorThreshold
        {
            get { return radialErrorThreshold; }
            set
            {
                if (value > 0.0)
                    radialErrorThreshold = value;
                else
                    throw new ArgumentOutOfRangeException("Value should be greater than zero");
            }
        }

        bool radialErrorThresholdExceeded = false;
        public bool RadialErrorThrehsoldExceeded
        {
            get { return radialErrorThresholdExceeded; }
            set
            {
                if (radialErrorThresholdExceeded != value)
                {
                    radialErrorThresholdExceeded = value;
                    RadialErrorThresholdExceededChanged.Rise(this, new EventArgs());
                }
            }
        }

        static bool nmeaSingleton = false;

        public bool IsOpen { get { return redNodePort.IsOpen; } }

        PortState redNodePortState = PortState.UNKNOWN;
        public PortState RedNODEPortState
        {
            get
            {
                return redNodePortState;
            }
            private set
            {
                if (redNodePortState != value)
                {
                    redNodePortState = value;
                    PortsStateChangedEvent.Rise(this, new EventArgs());
                }
            }
        }

        PortState auxPortState = PortState.CLOSED;
        public PortState AUXPortState
        {
            get
            {
                return auxPortState;
            }
            private set
            {
                if (auxPortState != value)
                {
                    auxPortState = value;
                    PortsStateChangedEvent.Rise(this, new EventArgs());
                }
            }
        }

        PortState outPortState = PortState.CLOSED;
        public PortState OutPortState
        {
            get
            {
                return outPortState;
            }
            private set
            {
                if (outPortState != value)
                {
                    outPortState = value;
                    PortsStateChangedEvent.Rise(this, new EventArgs());
                }
            }
        }

        bool redNodePortBusy = false;
        public bool RedNodePortBusy
        {
            get
            {
                return redNodePortBusy;
            }
            private set
            {
                if (value != redNodePortBusy)
                {
                    redNodePortBusy = value;
                    RedNODEPortBusyChanged.Rise(this, new EventArgs());
                }
            }
        }

        public string DeviceInfoString { get; private set; }
        public string DeviceSerial { get; private set; }
        bool isDeviceInfoUpdated = false;
        public bool IsDeviceInfoUpdated
        {
            get { return isDeviceInfoUpdated; }
            private set
            {
                if (isDeviceInfoUpdated != value)
                {
                    isDeviceInfoUpdated = value;
                    DeviceInfoUpdatedChangedEvent.Rise(this, new EventArgs());
                }
            }
        }

        AgingValue<GeoPoint> auxLocation;
        AgingValue<double> auxCourse;
        AgingValue<double> auxSpeed;

        AgingValue<double> courseToRedNode;
        AgingValue<double> courseToAux;
        AgingValue<double> distanceToRedNode;

        AgingValue<GeoPoint3DE> redNodeLocationRaw;
        AgingValue<GeoPoint3D> redNodeLocationFlt;
        AgingValue<double> redNodeCourse;
        AgingValue<double> redNodePressure;
        AgingValue<double> redNodeDepth;

        AgingValue<double> waterTemperature;

        AgingValue<GeoPoint3D> redBase1Location;
        AgingValue<GeoPoint3D> redBase2Location;
        AgingValue<GeoPoint3D> redBase3Location;
        AgingValue<GeoPoint3D> redBase4Location;

        AgingValue<BaseState> BaseStates;

        AgingValue<double> CEP;
        AgingValue<double> DRMS;
        AgingValue<double> DRMS2;
        AgingValue<double> DRMS3;

        delegate T NullChecker<T>(object parameter);
        delegate object NullCheckerR<T>(T parameter);
        NullChecker<int> intNullChecker = (x => x == null ? -1 : (int)x);
        NullChecker<double> doubleNullChecker = (x => x == null ? double.NaN : (double)x);
        NullChecker<string> stringNullChecker = (x => x == null ? string.Empty : (string)x);
        NullCheckerR<double> doubleNullCheckerR = (x => double.IsNaN(x) ? null : (object)x);

        double salinityPSU = 0.0;
        public double SalinityPSU
        {
            get
            {
                return salinityPSU;
            }
            set
            {
                if ((value >= 0.0) && (value <= 42.0))
                {
                    if (salinityPSU != value)
                    {
                        salinityPSU = value;
                        salinityUpdated = false;
                    }
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        bool salinityUpdated = false;
        
        bool isAutoSoundSpeed = true;
        double soundSpeed = 1450;
        public double SoundSpeed
        {
            get { return soundSpeed; }
            set
            {
                if ((value >= 1300) && (value <= 1700))
                {
                    soundSpeed = value;
                    isAutoSoundSpeed = false;
                    soundSpeedUpdated = false;
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        bool soundSpeedUpdated = true;

        bool isStatistics = false;
        public bool IsStatistics
        {
            get { return isStatistics; }
            set
            {
                if (value)
                {
                    statHelper.Clear();
                    CEP = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
                    DRMS = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
                    DRMS2 = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
                    DRMS3 = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
                }

                isStatistics = value;
            }
        }

        public readonly int StatHelperRingSize = 512;
        List<GeoPoint> statHelper = new List<GeoPoint>();

        public bool UPositionValid
        {
            get { return redNodeLocationFlt.IsInitialized; }
        }
        
        #endregion

        #region Constructor

        public CCore(SerialPortSettings redNodePortSettings, int courseEstimatorFIFOSize, int smoothingFilterFIFOSize, double resetFilterDistance_m)
        {
            #region Misc.

            portNameByHash = new Dictionary<int, string>();
            portTypeByHash = new Dictionary<int, PortType>();
            portHashByName = new Dictionary<string, int>();

            RedNODEPortState = PortState.CLOSED;
            AUXPortState = PortState.NOT_USED;
            OutPortState = PortState.NOT_USED;

            AddPortToDictionary(emuPortNameAUX, PortType.EMU_AUX);
            AddPortToDictionary(emuPortNameRDN, PortType.EMU_RN);
            AddPortToDictionary(redNodePortSettings.PortName, PortType.RedNODE);

            emuPortAUXID = portHashByName[emuPortNameAUX];
            emuPortRDNID = portHashByName[emuPortNameRDN];

            crsEstimator = new CourseEstimatorLA2D(courseEstimatorFIFOSize);
            trkSmoother = new TrackFilter(smoothingFilterFIFOSize, resetFilterDistance_m);

            #endregion

            #region Agings

            auxLocation = new AgingValue<GeoPoint>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "LAT: {0:F06}°\r\nLON: {1:F06}°", x.Latitude, x.Longitude));
            auxCourse = new AgingValue<double>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F01}°", x));
            auxSpeed = new AgingValue<double>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F01} m/s", x));

            courseToRedNode = new AgingValue<double>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F01}°", x));
            courseToAux = new AgingValue<double>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F01}°", x));
            distanceToRedNode = new AgingValue<double>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));

            redNodeLocationRaw = new AgingValue<GeoPoint3DE>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture, 
                "LAT: {0:F06}°\r\nLON: {1:F06}°\r\nDPT: {2:F03} m\r\nRER: {3:F03} m", 
                x.Latitude, x.Longitude, x.Depth, x.RadialError));

            redNodeLocationFlt = new AgingValue<GeoPoint3D>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture,
                "LAT: {0:F06}°\r\nLON: {1:F06}°\r\nDPT: {2:F03} m",
                x.Latitude, x.Longitude, x.Depth));
            redNodeCourse = new AgingValue<double>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F01}°", x));
            redNodePressure = new AgingValue<double>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F02} mBar", x));
            redNodeDepth = new AgingValue<double>(2, 15, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F02} m", x));
            waterTemperature = new AgingValue<double>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "{0:F01} °C", x));
            
            redBase1Location = new AgingValue<GeoPoint3D>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "LAT: {0:F06}°\r\nLON: {1:F06}°", x.Latitude, x.Longitude));
            redBase2Location = new AgingValue<GeoPoint3D>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "LAT: {0:F06}°\r\nLON: {1:F06}°", x.Latitude, x.Longitude));
            redBase3Location = new AgingValue<GeoPoint3D>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "LAT: {0:F06}°\r\nLON: {1:F06}°", x.Latitude, x.Longitude));
            redBase4Location = new AgingValue<GeoPoint3D>(2, 5, (x) => string.Format(CultureInfo.InvariantCulture, "LAT: {0:F06}°\r\nLON: {1:F06}°", x.Latitude, x.Longitude));

            BaseStates = new AgingValue<BaseState>(2, 5, (x) =>
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("#1 {0}", x.Base1Status);
                    if (!double.IsNaN(x.Base1MSR))
                        sb.AppendFormat(CultureInfo.InvariantCulture, "/{0:F01} db", x.Base1MSR);
                    sb.Append(", ");
                    sb.AppendFormat("#2 {0}", x.Base2Status);
                    if (!double.IsNaN(x.Base2MSR))
                        sb.AppendFormat(CultureInfo.InvariantCulture, "/{0:F01} db", x.Base2MSR);
                    sb.Append(", ");
                    sb.AppendFormat("#3 {0}", x.Base3Status);
                    if (!double.IsNaN(x.Base3MSR))
                        sb.AppendFormat(CultureInfo.InvariantCulture, "/{0:F01} db", x.Base3MSR);
                    sb.Append(", ");
                    sb.AppendFormat("#4 {0}", x.Base4Status);
                    if (!double.IsNaN(x.Base4MSR))
                        sb.AppendFormat(CultureInfo.InvariantCulture, "/{0:F01} db", x.Base4MSR);

                    return sb.ToString();
                });

            CEP = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
            DRMS = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
            DRMS2 = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));
            DRMS3 = new AgingValue<double>(4, 300, x => string.Format(CultureInfo.InvariantCulture, "{0:F03} m", x));

            #endregion

            #region NMEA

            if (!nmeaSingleton)
            {
                NMEAParser.AddManufacturerToProprietarySentencesBase(ManufacturerCodes.TNT);

                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "0", "x,x");                    // ACK
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "1", "x,x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "2", "x,x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "3", "x,x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "4", "xx,xx");                  // LOC_DATA_GET
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "5", "x,x.x");                  // LOC_DATA_VAL
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "6", "xx,xx");                  // LOC_ACT_INVOKE

                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "!", "c--c,x,c--c,x,x,c--c");   // DEVICE_INFO

                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "C", "x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "M", "x.x,x.x,x.x,x,x.x,x.x,x.x,x,x.x,x.x,x.x,x,x.x,x.x,x.x,x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "N", "x.x,x.x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "O", "x.x,x.x");
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "P", "xx,x.x");                 // SETVAL
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "Q", "x,x,x,x,x,x,x");
            }

            #endregion

            #region listener

            listener = new NMEAMultipleListener();
            listener.NMEAIncomingMessageReceived += (o, e) => LogEvent.Rise(this,
                new LogEventArgs(LogLineType.INFO,
                    string.Format("{0} ({1}) >> {2}",
                    portNameByHash[e.SourceID],
                    portTypeByHash[e.SourceID],
                    e.Message)));

            listener.RMCSentenceReceived += new EventHandler<RMCMessageEventArgs>(listener_RMCSentenceReceived);
            listener.GGASentenceReceived += new EventHandler<GGAMessageEventArgs>(listener_GGASentenceReceived);
            listener.MTWSentenceReceived += new EventHandler<MTWMessageEventArgs>(listener_MTWSentenceReceived);
            listener.NMEAProprietaryUnsupportedSentenceParsed += new EventHandler<NMEAUnsupportedProprietaryEventArgs>(listener_ProprietaryReceived);

            #endregion

            #region redNodePort

            redNodePort = new NMEASerialPort(redNodePortSettings);
            redNodePort.NewNMEAMessage += (o, e) =>
                {
                    redNodePortTS = 0;
                    RedNODEPortState = PortState.OK;
                    listener.ProcessIncoming(portHashByName[redNodePort.PortName], e.Message);
                };
            redNodePort.PortError += (o, e) => LogEvent.Rise(this,
                new LogEventArgs(LogLineType.ERROR,
                    string.Format("{0} in {1} ({2})",
                    e.EventType,
                    redNodePort.PortName,
                    PortType.RedNODE)));

            #endregion

            #region timer

            timer = new PrecisionTimer();
            timer.Mode = Mode.Periodic;
            timer.Period = timerPeriodMS;

            timer.Tick += (o, e) =>
                {
                    if (redNodePort.IsOpen)
                    {
                        redNodePortTS += timerPeriodMS;
                        if (redNodePortTS > portsTimeoutMS)
                        {
                            redNodePortTS = 0;                            
                            RedNODEPortState = PortState.TIMEOUT;
                            RedNodePortBusy = false;
                        }                       
                    }

                    if ((auxPort != null) && (auxPort.IsOpen))
                    {
                        auxPortTS += timerPeriodMS;
                        if (auxPortTS > portsTimeoutMS)
                        {
                            auxPortTS = 0;
                            auxPortState = PortState.TIMEOUT;
                        }
                    }

                    systemUpdateTS += timerPeriodMS;
                    if (systemUpdateTS > systemUpdateTimeoutMS)
                    {
                        systemUpdateTS = 0;
                        SystemUpdateEvent.Rise(this, new EventArgs());
                        OnMaintenance();
                    }
                };

            timer.Start();

            #endregion
        }

        #endregion

        #region Methods

        #region Private

        private string BCDVersionToStr(int versionData)
        {
            return string.Format("{0}.{1}", (versionData >> 0x08).ToString(), (versionData & 0xff).ToString("X2"));
        }

        private void OnSystemUpdated()
        {
            systemUpdateTS = 0;
            SystemUpdateEvent.Rise(this, new EventArgs());
        }

        private bool IsValidLat(double lt)
        {
            return (lt >= -90) && (lt <= 90);
        }

        private bool IsValidLon(double ln)
        {
            return (ln >= -180) && (ln <= 180);
        }

        private void AddPortToDictionary(string portName, PortType portType)
        {
            var portNameHash = portName.GetHashCode();
            portNameByHash.Add(portNameHash, portName);
            portHashByName.Add(portName, portNameHash);
            portTypeByHash.Add(portNameHash, portType);
        }

        private void WriteOutput(double lat_deg, double lon_deg, double dpt_m, double rer_m, double crs, bool isValid, DateTime timeStamp)
        {       
            string latCardinal, lonCardinal;

            string RMCvString;
            if (isValid) RMCvString = "Valid";
            else RMCvString = "Invalid";

            if (lat_deg > 0) latCardinal = "N";
            else latCardinal = "S";

            if (lon_deg > 0) lonCardinal = "E";
            else lonCardinal = "W";

            StringBuilder emuString = new StringBuilder();

            #region RMC

            emuString.Append(NMEAParser.BuildSentence(TalkerIdentifiers.GP,
                SentenceIdentifiers.RMC,
                new object[] 
                {
                    timeStamp, 
                    RMCvString, 
                    doubleNullCheckerR(Math.Abs(lat_deg)), latCardinal,
                    doubleNullCheckerR(Math.Abs(lon_deg)), lonCardinal,
                    null, // speed
                    doubleNullCheckerR(crs), // track true
                    timeStamp,
                    null, // magnetic variation
                    null, // magnetic variation direction
                    "A",
                }));

            #endregion

            #region GGA

            emuString.Append(NMEAParser.BuildSentence(TalkerIdentifiers.GP,
                SentenceIdentifiers.GGA,
                new object[]
                {
                    timeStamp,
                    doubleNullCheckerR(Math.Abs(lat_deg)), latCardinal,
                    doubleNullCheckerR(Math.Abs(lon_deg)), lonCardinal,
                    "GPS fix",
                    4,
                    doubleNullCheckerR(rer_m),
                    doubleNullCheckerR(-dpt_m),
                    "M",
                    null,
                    "M",
                    null,
                    null
                }));

            #endregion

            var emuStr = emuString.ToString();

            try
            {
                outPort.SendData(emuStr);
                LogEvent.Rise(this, new LogEventArgs(LogLineType.INFO, string.Format("{0} (OUT) << {1}", outPort.PortName, emuStr)));
            }
            catch (Exception ex)
            {
                LogEvent.Rise(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }


        private void Parse_TNTM(object[] parameters)
        {
            double[] blat = new double[4];
            double[] blon = new double[4];
            double[] bmsr = new double[4];
            RedBASEStatus[] bsts = new RedBASEStatus[4];

            for (int i = 0; i < 4; i++)
            {
                bsts[i] = (RedBASEStatus)(int)parameters[3 + i * 4];
                if ((bsts[i] == RedBASEStatus.ALIVE) ||
                    (bsts[i] == RedBASEStatus.BAT_LOW) ||
                    (bsts[i] == RedBASEStatus.OK))
                {
                    blat[i] = (double)parameters[0 + i * 4]; blat[i] = IsValidLat(blat[i]) ? blat[i] : double.NaN;
                    blon[i] = (double)parameters[1 + i * 4]; blon[i] = IsValidLon(blon[i]) ? blon[i] : double.NaN;
                    bmsr[i] = (double)parameters[2 + i * 4];
                }
                else
                {
                    blat[i] = double.NaN;
                    blon[i] = double.NaN;
                    bmsr[i] = double.NaN;
                }
            }

            BaseStates.Value = new BaseState(bsts[0], bmsr[0], bsts[1], bmsr[1], bsts[2], bmsr[2], bsts[3], bmsr[3]);

            if (!double.IsNaN(blat[0]) && !double.IsNaN(blon[0]))
            {
                redBase1Location.Value = new GeoPoint3D(blat[0], blon[0], RedWAVE.RedBASE_DEFAULT_DPT_M);
                TrackUpdatedEvent.Rise(this, new TrackUpdateEventArgs("BASE 1", new GeoPoint3DETm(blat[0], blon[0], 0, 0, DateTime.Now)));
            }
            if (!double.IsNaN(blat[1]) && !double.IsNaN(blon[1]))
            {
                redBase2Location.Value = new GeoPoint3D(blat[1], blon[1], RedWAVE.RedBASE_DEFAULT_DPT_M);
                TrackUpdatedEvent.Rise(this, new TrackUpdateEventArgs("BASE 2", new GeoPoint3DETm(blat[1], blon[1], 0, 0, DateTime.Now)));
            }
            if (!double.IsNaN(blat[2]) && !double.IsNaN(blon[2]))
            {
                redBase3Location.Value = new GeoPoint3D(blat[2], blon[2], RedWAVE.RedBASE_DEFAULT_DPT_M);
                TrackUpdatedEvent.Rise(this, new TrackUpdateEventArgs("BASE 3", new GeoPoint3DETm(blat[2], blon[2], 0, 0, DateTime.Now)));
            }
            if (!double.IsNaN(blat[3]) && !double.IsNaN(blon[3]))
            {
                redBase4Location.Value = new GeoPoint3D(blat[3], blon[3], RedWAVE.RedBASE_DEFAULT_DPT_M);
                TrackUpdatedEvent.Rise(this, new TrackUpdateEventArgs("BASE 4", new GeoPoint3DETm(blat[3], blon[3], 0, 0, DateTime.Now)));
            }

            OnSystemUpdated();            
        }

        private void Parse_ACK(object[] parameters)
        {
            RedWAVEErrorCode errCode = (RedWAVEErrorCode)(int)parameters[0];
            LogEvent.Rise(this, new LogEventArgs(LogLineType.INFO, string.Format("RedNODE Query result: {0}", errCode)));
            RedNodePortBusy = false;
        }

        private void Parse_DINFO(object[] parameters)
        {
            int dVer = (int)parameters[1];
            int cVer = (int)parameters[3];
            DeviceInfoString = string.Format("{0} v{1}, {2} v{3}",
                (string)parameters[0], BCDVersionToStr(dVer),
                (string)parameters[2], BCDVersionToStr(cVer));
            DeviceSerial = (string)parameters[5];

            IsDeviceInfoUpdated = true;
            RedNodePortBusy = false;
        }

        private void Parse_LOCVAL(object[] parameters)
        {
            RedWAVELocalDataID dataID = (RedWAVELocalDataID)intNullChecker(parameters[0]);
            double data = doubleNullChecker(parameters[1]);

            if (dataID == RedWAVELocalDataID.SALINITY)
                salinityUpdated = true;
            else if (dataID == RedWAVELocalDataID.SOUND_SPEED)
                soundSpeedUpdated = true;

            RedNodePortBusy = false;            
        }

        private void Parse_TNTO(object[] parameters)
        {
            double prs = doubleNullChecker(parameters[0]);

            if (!double.IsNaN(prs))
                redNodePressure.Value = prs;
        }

        private void Parse_TNTN(object[] parameters)
        {
            double dpt = doubleNullChecker(parameters[0]);

            if (!double.IsNaN(dpt))
                 redNodeDepth.Value = dpt;
        }

        private void TryUpdateRelatives()
        {
            if (auxLocation.IsInitialized && redNodeLocationFlt.IsInitialized)
            {
                double auxLat_rad = Algorithms.Deg2Rad(auxLocation.Value.Latitude);
                double auxLon_rad = Algorithms.Deg2Rad(auxLocation.Value.Longitude);
                double rnLat_rad = Algorithms.Deg2Rad(redNodeLocationFlt.Value.Latitude);
                double rnLon_rad = Algorithms.Deg2Rad(redNodeLocationFlt.Value.Longitude);

                double dst_m = 0.0;
                double fwd_az_rad = 0.0;
                double rev_az_rad = 0.0;
                int its = 0;

                Algorithms.VincentyInverse(auxLat_rad, auxLon_rad, rnLat_rad, rnLon_rad,
                    Algorithms.WGS84Ellipsoid,
                    Algorithms.VNC_DEF_EPSILON, Algorithms.VNC_DEF_IT_LIMIT,
                    out dst_m, out fwd_az_rad, out rev_az_rad, out its);

                rev_az_rad += Math.PI;
                if (rev_az_rad > Math.PI * 2)
                    rev_az_rad -= Math.PI * 2;
                
                distanceToRedNode.Value = dst_m;
                courseToRedNode.Value = Algorithms.Rad2Deg(fwd_az_rad);
                courseToAux.Value = Algorithms.Rad2Deg(rev_az_rad);

                OnSystemUpdated();
            }
        }

        private void SendToRedNode(string msg)
        {
            if (!redNodePortBusy && IsOpen)
            {
                try
                {
                    redNodePort.SendData(msg);
                    RedNodePortBusy = true;
                    LogEvent.Rise(this, new LogEventArgs(LogLineType.INFO, string.Format("{0} ({1}) << {2}", redNodePort.PortName, PortType.RedNODE, msg)));
                }
                catch (Exception ex)
                {
                    LogEvent.Rise(this, new LogEventArgs(LogLineType.ERROR, string.Format("{0} ({1}) >> {2}", redNodePort.PortName, PortType.RedNODE, ex.Message)));
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to send in this state of the port");
            }
        }


        private void QuerySetSalinity(double sty)
        {
            SendToRedNode(NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "P", new object[] { (int)RedWAVELocalDataID.SALINITY, sty }));
        }

        private void QuerySetSpeedOfSound(double speed_mps)
        {
            SendToRedNode(NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "4", new object[] { (int)RedWAVELocalDataID.SOUND_SPEED, speed_mps }));
        }

        private void OnMaintenance()
        {
            if (IsOpen && !redNodePortBusy)
            {
                if (!salinityUpdated)
                    QuerySetSalinity(SalinityPSU);
                else if (!soundSpeedUpdated)
                    QuerySetSpeedOfSound(soundSpeed);
            }
        }

        #endregion

        #region Public

        public void InitAuxPort(SerialPortSettings portSettings)
        {
            AddPortToDictionary(portSettings.PortName, PortType.AUX);

            auxPort = new NMEASerialPort(portSettings);
            auxPort.NewNMEAMessage += (o, e) => 
                {
                    auxPortTS = 0;
                    AUXPortState = PortState.OK;
                    listener.ProcessIncoming(portHashByName[auxPort.PortName], e.Message);
                };
            auxPort.PortError += (o, e) =>
                {
                    AUXPortState = PortState.ERROR;
                    LogEvent.Rise(this,
                        new LogEventArgs(LogLineType.ERROR,
                            string.Format("{0} in {1} ({2})",
                            e.EventType,
                            auxPort.PortName,
                            PortType.AUX)));
                };

            AUXPortState = PortState.CLOSED;
        }

        public void InitOutPort(SerialPortSettings portSettings)
        {
            AddPortToDictionary(portSettings.PortName, PortType.OUT);

            outPort = new NMEASerialPort(portSettings);
            outPort.PortError += (o, e) =>
                {
                    OutPortState = PortState.ERROR;
                    LogEvent.Rise(this,
                        new LogEventArgs(LogLineType.ERROR,
                            string.Format("{0} in {1} ({2})",
                            e.EventType,
                            outPort.PortName,
                            PortType.OUT)));
                };

            OutPortState = PortState.CLOSED;
        }

        public void Open()
        {
            if (!IsOpen)
            {
                redNodePort.Open();
                IsDeviceInfoUpdated = false;
                redNodePortTS = 0;
                RedNODEPortState = PortState.OPEN;

                if (auxPort != null)
                {
                    try
                    {
                        auxPort.Open();
                        AUXPortState = PortState.OPEN;
                        auxPortTS = 0;
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(this,
                            new LogEventArgs(LogLineType.ERROR,
                                string.Format("Unable to open {0} ({1}): {2}",
                                auxPort.PortName, PortType.AUX, ex.Message)));
                    }
                }

                if (outPort != null)
                {
                    try
                    {
                        outPort.Open();
                        OutPortState = PortState.OPEN;
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(this,
                            new LogEventArgs(LogLineType.ERROR,
                                string.Format("Unable to open {0} ({1}): {2}",
                                outPort.PortName, PortType.OUT, ex.Message)));
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Connection(s) already opened");
            }

            if (redNodePort.IsOpen)
                OnMaintenance();
        }

        public void Close()
        {
            if (IsOpen)
            {
                try
                {
                    redNodePort.Close();
                    RedNODEPortState = PortState.CLOSED;

                    if (!isAutoSoundSpeed)
                        soundSpeedUpdated = false;

                    salinityUpdated = false;
                }
                catch (Exception ex)
                {
                    LogEvent.Rise(this,
                        new LogEventArgs(LogLineType.ERROR,
                            string.Format("Error closing {0} ({1}): {2}",
                            redNodePort.PortName, PortType.RedNODE, ex.Message)));
                }

                if (auxPort != null)
                {
                    try
                    {
                        auxPort.Close();
                        AUXPortState = PortState.CLOSED;
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(this,
                        new LogEventArgs(LogLineType.ERROR,
                            string.Format("Error closing {0} ({1}): {2}",
                            auxPort.PortName, PortType.AUX, ex.Message)));
                    }
                }

                if (outPort != null)
                {
                    try
                    {
                        outPort.Close();
                        OutPortState = PortState.CLOSED;
                    }
                    catch (Exception ex)
                    {
                        LogEvent.Rise(this,
                        new LogEventArgs(LogLineType.ERROR,
                            string.Format("Error closing {0} ({1}): {2}",
                            outPort.PortName, PortType.OUT, ex.Message)));
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Connection(s) closed");
            }
        }

        public void MarkPosition()
        {
            if (redNodeLocationFlt.IsInitialized)
                TrackUpdatedEvent.Rise(this, new TrackUpdateEventArgs("Marked",
                    new GeoPoint3DETm(redNodeLocationFlt.Value.Latitude, redNodeLocationFlt.Value.Longitude, redNodeLocationFlt.Value.Depth, 0.0, redNodeLocationFlt.TimeStamp)));
        }

        public string GetPortsStateDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("RDN ({0}): {1}", redNodePort.PortName, redNodePortState);
            if (auxPortState != PortState.NOT_USED)
                sb.AppendFormat(", AUX ({0}): {1}", auxPort.PortName, auxPortState);
            if (outPortState != PortState.NOT_USED)
                sb.AppendFormat(", OUT ({0}): {1}", outPort.PortName, outPortState);

            return sb.ToString();
        }

        public void Emulate(string emuMsg)
        {
            if (emuMsg.Contains("INFO"))
            {
                var splits = emuMsg.Split(charSeps);

                if (splits[1].Contains("INFO") &&
                    (splits[4] == ">>"))
                {
                    var nmeaSnt = splits[5];
                    if (!nmeaSnt.EndsWith(NMEAParser.SentenceEndDelimiter))
                        nmeaSnt = nmeaSnt + NMEAParser.SentenceEndDelimiter;

                    var sourceIDStr = splits[3].Trim(trimChars);
                    PortType sourcePType = (PortType)Enum.Parse(typeof(PortType), sourceIDStr);

                    if (sourcePType == PortType.RedNODE)
                        listener.ProcessIncoming(emuPortRDNID, nmeaSnt);
                    else if (sourcePType == PortType.AUX)
                        listener.ProcessIncoming(emuPortAUXID, nmeaSnt);                      
                }
            }
        }


        public string GetBasesDescription()
        {
            return BaseStates.ToString();
        }

        public string GetNavigationInfo()
        {
            StringBuilder sb = new StringBuilder();

            if (auxCourse.IsInitialized &&
                auxSpeed.IsInitialized)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture,     "    AUX TRACK: {0}\r\n", auxCourse);
                if (courseToRedNode.IsInitialized)
                    sb.AppendFormat(CultureInfo.InvariantCulture, "COURSE TO UNR: {0}\r\n\r\n", courseToRedNode);
                if (distanceToRedNode.IsInitialized)
                    sb.AppendFormat(CultureInfo.InvariantCulture, "  DIST TO UNR: {0}\r\n", distanceToRedNode);
                sb.AppendFormat(CultureInfo.InvariantCulture,     "    AUX SPEED: {0}\r\n\r\n", auxSpeed);
            }

            if (redNodeCourse.IsInitialized)
            {                
                sb.AppendFormat(CultureInfo.InvariantCulture,     "    UNR TRACK: {0}\r\n", redNodeCourse);

                if (courseToAux.IsInitialized)
                    sb.AppendFormat(CultureInfo.InvariantCulture, "COURSE TO AUX: {0}\r\n", courseToAux);
            }            

            return sb.ToString();
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            if (isStatistics)
            {
                if (CEP.IsInitialized)
                    sb.AppendFormat("  CEP: {0}\r\n", CEP);

                if (DRMS.IsInitialized)
                    sb.AppendFormat(" DRMS: {0}\r\n", DRMS);

                if (DRMS2.IsInitialized)
                    sb.AppendFormat("2DRMS: {0}\r\n", DRMS2);

                if (DRMS3.IsInitialized)
                    sb.AppendFormat("3DRMS: {0}\r\n", DRMS3);
            }

            return sb.ToString();
        }

        public string GetRNLocationInfo()
        {
            if (redNodeLocationRaw.IsInitialized)
                return redNodeLocationRaw.ToString();
            else
                return string.Empty;
        }

        public string GetAmbInfo()
        {
            StringBuilder sb = new StringBuilder();

            if (redNodeDepth.IsInitialized)
                sb.AppendFormat("DPT: {0}\r\n", redNodeDepth);

            if (redNodePressure.IsInitialized)
                sb.AppendFormat("PRS: {0}\r\n", redNodePressure);

            if (waterTemperature.IsInitialized)
                sb.AppendFormat("MTW: {0}\r\n", waterTemperature);

            return sb.ToString();
        }
        
        public void QueryDeviceInfo()
        {
            SendToRedNode(NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "4", new object[] { (int)RedWAVELocalDataID.DEVICE_INFO, 0 }));
        }

        public void Reset()
        {
            trkSmoother.Reset();
        }

        public void UpdateStat(double lat, double lon)
        {
            statHelper.Add(new GeoPoint(lat, lon));
            if (statHelper.Count > StatHelperRingSize)
                statHelper.RemoveAt(0);

            var mPoints = Navigation.GCSToLCS(statHelper, Algorithms.WGS84Ellipsoid);

            double sigmax = 0, sigmay = 0;
            Navigation.GetPointsSTD2D(mPoints, out sigmax, out sigmay);

            CEP.Value = Navigation.CEP(sigmax, sigmay);
            var drms = Navigation.DRMS(sigmax, sigmay);
            DRMS.Value = drms;
            DRMS2.Value = drms * 2;
            DRMS3.Value = drms * 3;
        }

        #endregion

        #endregion

        #region Handlers

        #region listener

        private void listener_RMCSentenceReceived(object sender, RMCMessageEventArgs e)
        {
            if (e.IsValid)
            {
                if ((portTypeByHash[e.SourceID] == PortType.AUX) ||
                    (portTypeByHash[e.SourceID] == PortType.EMU_AUX))                    
                {
                    TrackUpdatedEvent.Rise(this,
                        new TrackUpdateEventArgs("AUX GNSS",
                            new GeoPoint3DETm(e.Latitude, e.Longitude, 0.0, 0.0, e.TimeFix),
                            e.TrackTrue, e.SpeedKmh / 3.6));

                    auxLocation.Value = new GeoPoint(e.Latitude, e.Longitude);
                    auxCourse.Value = e.TrackTrue;
                    auxSpeed.Value = e.SpeedKmh / 3.6;

                    TryUpdateRelatives();
                    OnSystemUpdated();
                }
            }
        }
        
        private void listener_MTWSentenceReceived(object sender, MTWMessageEventArgs e)
        {
            if (e.IsValid)
            {
                waterTemperature.Value = e.MeanWaterTemperature;
                OnSystemUpdated();
            }
        }

        private void listener_GGASentenceReceived(object sender, GGAMessageEventArgs e)
        {
            if ((portTypeByHash[e.SourceID] == PortType.RedNODE) ||
                (portTypeByHash[e.SourceID] == PortType.EMU_RN))
            {
                double crs = double.NaN;
                double outLat = e.Latitude;
                double outLon = e.Longitude;                               

                if (!double.IsNaN(e.HDOP) && (e.HDOP <= radialErrorThreshold))
                {
                    redNodeLocationRaw.Value = new GeoPoint3DE(e.Latitude, e.Longitude, -e.OrthometricHeight, e.HDOP);

                    crsEstimator.AddPoint(new GeoPoint(e.Latitude, e.Longitude));
                    if (crsEstimator.IsCourse)
                    {
                        crs = crsEstimator.Course_deg;
                        redNodeCourse.Value = crs;
                    }

                    TrackUpdatedEvent.Rise(this,
                        new TrackUpdateEventArgs("UNR (RAW)",
                            new GeoPoint3DETm(e.Latitude, e.Longitude, -e.OrthometricHeight, e.HDOP, e.TimeStamp),
                            crs, double.NaN));

                    var smPoint = trkSmoother.Filter(e.Latitude, e.Longitude);
                    TrackUpdatedEvent.Rise(this,
                        new TrackUpdateEventArgs("UNR (FLT)",
                            new GeoPoint3DETm(smPoint.Latitude, smPoint.Longitude, -e.OrthometricHeight, e.HDOP, e.TimeStamp),
                            crs, double.NaN));

                    redNodeLocationFlt.Value = new GeoPoint3D(smPoint.Latitude, smPoint.Longitude, -e.OrthometricHeight);

                    if (isStatistics)
                    {
                        UpdateStat(smPoint.Latitude, smPoint.Longitude);
                    }

                    outLat = smPoint.Latitude;
                    outLon = smPoint.Longitude;

                    TryUpdateRelatives();
                    OnSystemUpdated();
                }

                RadialErrorThrehsoldExceeded = e.HDOP > radialErrorThreshold;

                if ((outPort != null) && (outPort.IsOpen))
                    WriteOutput(outLat, outLon, e.OrthometricHeight, e.HDOP, crs, (e.HDOP < radialErrorThreshold) && !double.IsNaN(e.HDOP), e.TimeStamp);
            }
        }

        private void listener_ProprietaryReceived(object sender, NMEAUnsupportedProprietaryEventArgs e)
        {
            if ((portTypeByHash[e.SourceID] == PortType.RedNODE) ||
                (portTypeByHash[e.SourceID] == PortType.EMU_RN))
            {
                if (e.Sentence.Manufacturer == ManufacturerCodes.TNT)
                {
                    if (e.Sentence.SentenceIDString == "M") // Bouy's status
                        Parse_TNTM(e.Sentence.parameters);
                    else if (e.Sentence.SentenceIDString == "0") // ACK
                        Parse_ACK(e.Sentence.parameters);
                    else if (e.Sentence.SentenceIDString == "!") // DEVICE_INFO
                        Parse_DINFO(e.Sentence.parameters);
                    else if (e.Sentence.SentenceIDString == "5") // LOC_VAL
                        Parse_LOCVAL(e.Sentence.parameters);
                    else if (e.Sentence.SentenceIDString == "O") // PRETMP
                        Parse_TNTO(e.Sentence.parameters);
                    else if (e.Sentence.SentenceIDString == "N") // DPTTMP
                        Parse_TNTN(e.Sentence.parameters);
                }
            }
        }

        #endregion

        #endregion

        #region Events

        public EventHandler<LogEventArgs> LogEvent;
        public EventHandler<TrackUpdateEventArgs> TrackUpdatedEvent;
        public EventHandler RadialErrorThresholdExceededChanged;
        public EventHandler PortsStateChangedEvent;
        public EventHandler SystemUpdateEvent;
        public EventHandler RedNODEPortBusyChanged;
        public EventHandler DeviceInfoUpdatedChangedEvent;

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    #region timer

                    if (timer.IsRunning)
                    {
                        timer.Stop();
                    }
                    timer.Dispose();

                    #endregion

                    #region redNodePort

                    if (redNodePort != null)
                    {
                        try
                        {
                            redNodePort.Close();
                        }
                        catch { }

                        redNodePort.Dispose();
                    }

                    #endregion

                    #region auxPort

                    if (auxPort != null)
                    {
                        if (auxPort.IsOpen)
                        {
                            try
                            {
                                auxPort.Close();
                            }
                            catch { }

                            auxPort.Dispose();

                        }
                    }

                    #endregion

                    #region outPort

                    if (outPort != null)
                    {
                        if (outPort.IsOpen)
                        {
                            try
                            {
                                outPort.Close();
                            }
                            catch { }
                        }

                        outPort.Dispose();
                    }

                    #endregion
                }

                disposed = true;
            }
        }

        #endregion
    }
}
