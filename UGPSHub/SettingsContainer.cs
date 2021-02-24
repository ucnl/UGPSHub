using System;
using UCNLDrivers;

namespace UGPSHub
{
    [Serializable]
    public class SettingsContainer : SimpleSettingsContainer
    {
        #region Properties

        public string RedNODEPortName;
        public BaudRate RedNODEPortBaudrate;

        public bool IsUseAUXGNSS;
        public string AUXGNSSPortName;
        public BaudRate AUXGNSSPortBaudrate;

        public bool IsUseOutputPort;
        public string OutputPortName;
        public BaudRate OutputPortBaudrate;

        public double Salinity_psu;
        public bool IsAutoSoundspeed;
        public double SoundSpeed_mps;

        public double RadialErrorThreshold_m;
        public int CourseEstimatorFIFOSize;
        public int TrackFilterFIFOSize;

        public int NumberOfPointsToShow;

        public bool IsScreenshotsNamingByTime;

        public double MaxDistToResetTrackSmoother_m;

        public string[] TileServers;


        #endregion

        #region Methods

        public override void SetDefaults()
        {
            RedNODEPortName = "COM1";
            RedNODEPortBaudrate = BaudRate.baudRate9600;

            IsUseAUXGNSS = false;
            AUXGNSSPortName = "COM1";
            AUXGNSSPortBaudrate = BaudRate.baudRate9600;

            IsUseOutputPort = false;
            OutputPortName = "COM1";
            OutputPortBaudrate = BaudRate.baudRate9600;

            Salinity_psu = 0.0;
            IsAutoSoundspeed = true;
            SoundSpeed_mps = 1450.0;

            RadialErrorThreshold_m = 25.0;
            CourseEstimatorFIFOSize = 8;
            TrackFilterFIFOSize = 4;

            NumberOfPointsToShow = 256;

            IsScreenshotsNamingByTime = true;

            MaxDistToResetTrackSmoother_m = 100;

            TileServers = new string[]
            {
                "https://a.tile.openstreetmap.org",
                "https://b.tile.openstreetmap.org",
                "https://c.tile.openstreetmap.org"
            };
        }

        #endregion
    }
}
