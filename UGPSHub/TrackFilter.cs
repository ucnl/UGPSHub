using System;
using System.Collections.Generic;
using UCNLNav;

namespace UGPSHub
{
    public class TrackFilter
    {
        #region Properties

        public int FilterSize { get; private set; }

        List<MPoint> points;

        double anchor_lat_rad = double.NaN;
        double anchor_lon_rad = double.NaN;

        #endregion

        #region Constructor

        public TrackFilter(int filterSize)
        {
            if (filterSize <= 0)
                throw new ArgumentOutOfRangeException("filterSize should be greater than zero");

            FilterSize = filterSize;
            points = new List<MPoint>(filterSize);
        }

        #endregion

        #region Methods

        public void Reset()
        {
            points.Clear();
            anchor_lat_rad = double.NaN;
            anchor_lon_rad = double.NaN;
        }

        public GeoPoint Filter(double lat_deg, double lon_deg)
        {
            if (points.Count == 0)
            {
                anchor_lat_rad = Algorithms.Deg2Rad(lat_deg);
                anchor_lon_rad = Algorithms.Deg2Rad(lon_deg);
                points.Add(new MPoint(0, 0));
                return new GeoPoint(lat_deg, lon_deg);
            }
            else
            {
                if (points.Count >= FilterSize)
                    points.RemoveAt(0);

                double deltay = 0, deltax = 0;
                Algorithms.GetDeltasByGeopoints_WGS84(anchor_lat_rad, anchor_lon_rad,
                    Algorithms.Deg2Rad(lat_deg), Algorithms.Deg2Rad(lon_deg),
                    out deltay, out deltax);

                points.Add(new MPoint(deltax, deltay));

                double meanx = 0.0, meany = 0.0;
                for (int i = 0; i < points.Count; i++)
                {
                    meanx += points[i].X * (i + 1);
                    meany += points[i].Y * (i + 1);
                }

                double fWeight = (points.Count + points.Count * points.Count) / 2.0;
                meanx /= fWeight;
                meany /= fWeight;

                double lat_rad = double.NaN;
                double lon_rad = double.NaN;
                Algorithms.GeopointOffsetByDeltas_WGS84(anchor_lat_rad, anchor_lon_rad, meany, meanx, out lat_rad, out lon_rad);

                return new GeoPoint(Algorithms.Rad2Deg(lat_rad), Algorithms.Rad2Deg(lon_rad));
            }
        }

        #endregion
    }
}
