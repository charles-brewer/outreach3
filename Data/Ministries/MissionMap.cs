using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;

namespace outreach3.Data.Ministries
{

    public class MissionMap
    {

        public int MissionMapId { get; set; }
        public string? MapName { get; set; }
        public string? MapData { get; set; }
        public string? MapZoom { get; set; }
        public string? MapTilt { get; set; }
        public string? MapHeading { get; set; }
        public ICollection<MapMarker> MapMarkers { get; set; } = new List<MapMarker>();

    }



    public class MapMarker
    {
        public int MapMarkerId { get; set; }
        public int MissionMapId { get; set; } // Required foreign key property
        public MissionMap MissionMap { get; set; } = null!;
        public string? LatLng { get; set; }
        public string? Icon { get; set; }
        public int Number { get; set; }
        public int OneToEightNumber { get; set; }
        public string Color { get; set; } = "white";
        public string Title { get; set; } = "Not Available";
        public string? Area { get; set; }


        public string Label { get; set; } = "";


    }
}