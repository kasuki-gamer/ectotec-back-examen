using System;
using System.Collections.Generic;
using System.Text;

namespace ectotec.Models.ViewModels
{
    public class GeoNameResponse
    {
        public int totalResultsCount { get; set; }
        public List<GeoNames> geonames { get; set; }
    }
}
