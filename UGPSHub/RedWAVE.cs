using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGPSHub
{
    public enum RedWAVEDeviceType : int
    {
        RedBASE = 0,
        RedNODE = 1,
        RedNAV = 2,
        RedGTR = 3,
        INVALID
    }

    public enum RedWAVEErrorCode : int
    {
        NO_ERROR = 0,
        INVALID_SYNTAX = 1,
        UNSUPPORTED = 2,
        TRANSMITTER_BUSY = 3,
        ARGUMENT_OUT_OF_RANGE = 4,
        INVALID_OPERATION = 5,
        UNKNOWN_FIELD_ID = 6,
        VALUE_UNAVAILABLE = 7,
        RECEIVER_BUSY = 8,
        INVALID
    }

    public enum RedWAVELocalDataID : int
    {
        DEVICE_INFO = 0,
        MAX_REMOTE_TIMEOUT = 1,
        MAX_SUBSCRIBERS = 2,
        DEPTH = 3,
        TEMPERATURE = 4,
        BAT_CHARGE = 5,
        PRESSURE_RATING = 6,
        ZERO_PRESSURE = 7,
        WATER_DENSITY = 8,
        SALINITY = 9,
        SOUND_SPEED = 10,
        GRAVITY_ACC = 11,
        YEAR = 12,
        MONTH = 13,
        DATE = 14,
        HOUR = 15,
        MINUTE = 16,
        SECOND = 17,
        INVALID
    }

    public enum RedWAVEServiceActionID : int
    {
        INVOKE_FLASH_WRITE = 0,
        INVOKE_CLEAR_WAYPOINTS = 1,
        INVOKE_CLEAR_TRACK = 2,
        INVOKE_CLEAR_NDTABLE = 3,
        INVOKE_DPT_ADJUST = 4,
        INVALID
    }

    public enum RedBASEStatus : int
    {
        NO_DATA = 0,
        TMO = 1,
        BAT_LOW = 2,
        OK = 3,
        ALIVE = 4,
        INVALID
    }

    public static class RedWAVE
    {
        public static readonly double RedBASE_DEFAULT_DPT_M = 1.5;

    }
}
