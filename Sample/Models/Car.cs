
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sample.Models
{
    public class Car
    {        
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public IEnumerable<Feature> SelectedFeatures
        {
            get
            {
                foreach (var id in SelectedFeatureIds)
                    yield return Feature.AvailableFeatures.First(f => f.Id == id);
            }
        }

        public int Id { get; set; }
        public int[] SelectedFeatureIds { get; set; }
    }

    public class Feature
    {
        public static Feature[] AvailableFeatures
        {
            get { return new[] { new Feature { Id = 1, Name = "Cruise Control" }, new Feature { Id = 2, Name = "Power Steering" }, new Feature { Id = 3, Name = "MP3 Player" }, new Feature { Id = 4, Name = "Remote Locking" }, new Feature { Id = 5, Name = "Parking Sensors" }, new Feature { Id = 6, Name = "Fluffy Dice" } }; }
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}