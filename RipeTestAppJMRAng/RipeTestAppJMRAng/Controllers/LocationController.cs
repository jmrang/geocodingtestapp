using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RipeTestAppJMRAng.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RipeTestAppJMRAng.Helpers;

namespace RipeTestAppJMRAng.Controllers
{
    public class LocationController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Location
        public ActionResult Index()
        {
            var allList = db.Locations.ToList();

            return View(allList);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = CommonStrings.PostBindIncludeString)]Location location)
        {
            var normalizeStreetAddress = location.StreetAddress.Replace(CommonStrings.SingleSpace, CommonStrings.PlusSign);
            var normalizeCity = CommonStrings.PlusSign + location.City.Replace(CommonStrings.SingleSpace, CommonStrings.PlusSign);
            var normalizeState = CommonStrings.PlusSign + location.State.Replace(CommonStrings.SingleSpace, CommonStrings.PlusSign);
            var normalizeZipCode = CommonStrings.PlusSign + location.ZipCode.Replace(CommonStrings.SingleSpace, CommonStrings.PlusSign);

            var geocodeGoogleString = String.Format("{0}{1}{2}{3}{4}{5}", 
                CommonStrings.GoogleGeocodingRequestStrOne, 
                normalizeStreetAddress,
                normalizeCity,
                normalizeState,
                normalizeZipCode,
                CommonStrings.GoogleGeocodingRequestStrTwo);

            HttpWebRequest request = WebRequest.Create(geocodeGoogleString) as HttpWebRequest;

            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var responseReaderJson = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    while ((responseReaderJson = reader.ReadLine()) != null)
                    {
                        sb.Append(responseReaderJson);
                    }

                    var parseStringBuilder = JObject.Parse(sb.ToString());

                    var parseResult = parseStringBuilder[CommonStrings.ParamResult];
                    var geometry = parseResult[0][CommonStrings.ParamGeometry];
                    var geometryLocation = geometry[CommonStrings.ParamLocation];

                    var geoLocLong = geometryLocation[CommonStrings.ParamLng].ToString() ;
                    var geoLocLat = geometryLocation[CommonStrings.ParamLat].ToString();

                    location.Longitude = geoLocLong;
                    location.Lattitude = geoLocLat;

                    return SaveLocationToDatabase(location);
                }
            }
        }

        private ActionResult SaveLocationToDatabase(Location location)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.Locations.Add(location);
                    db.SaveChanges();
                    return RedirectToAction(CommonStrings.HomeIndexView);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(CommonStrings.Error, ex);
            }

            return View(location);
        }
    }
}