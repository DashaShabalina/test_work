using System;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Task1.Data;

namespace Task1
{
    public partial class Map : Form
    {
        private SqlConnection _connection;
        private GMapOverlay _markerLayer = new GMapOverlay("Marker");
        private GMapMarker _selectedMarker;
        public Map(SqlConnection connection)
        {
            InitializeComponent();

            _connection = connection;
            gmap.CanDragMap = true;
            gmap.DragButton = MouseButtons.Left;
            gmap.MaxZoom = 18;
            gmap.MinZoom = 2;
            gmap.MarkersEnabled = true;
            gmap.NegativeMode = false;
            gmap.Zoom = 5;
            gmap.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
        }

        private void Map_Load(object sender, EventArgs e)
        {
            gmap.MouseUp += _gMapControl_MouseUp;
            gmap.MouseDown += _gMapControl_MouseDown;
            gmap.Overlays.Add(_markerLayer);
        }

        private void _gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            _selectedMarker = gmap.Overlays
                .SelectMany(o => o.Markers)
                .FirstOrDefault(m => m.IsMouseOver == true);
        }

        private void _gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selectedMarker is null) return;

            var latlng = gmap.FromLocalToLatLng(e.X, e.Y);

            string sqlExpression = string.Format("Update Location Set latitude={0} , longitude={1} " +
                "Where latitude = {2} AND " +
                "longitude = {3} AND " +
                "id_equipment = {4}",
                latlng.Lat, latlng.Lng, _selectedMarker.Position.Lat, _selectedMarker.Position.Lng, Convert.ToInt32(_selectedMarker.ToolTipText));
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _selectedMarker.Position = latlng;
            _selectedMarker = null;
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            var ListPoints = new List<Point>();
            SqlCommand cmd = new SqlCommand("Select * FROM Location", _connection);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    ListPoints.Add(new Point(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), Convert.ToDouble(reader[2]), Convert.ToDouble(reader[3])));
                }
            reader.Close();

            for (int i = 0; i < ListPoints.Count; i++)
            {
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(ListPoints[i].Latitude, ListPoints[i].Longitude), GMarkerGoogleType.red);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.ToolTipText = Convert.ToString(ListPoints[i].IdEquipment);
                _markerLayer.Markers.Add(marker);
            }
            gmap.Overlays.Add(_markerLayer);
        }
    }
}