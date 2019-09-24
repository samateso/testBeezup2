using BeezupAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.SessionState;

namespace BeezupAPI.Controllers
{
    public class FilterController : ApiController
    {
        private readonly IFilterManager _filterManager;
        public FilterController(IFilterManager filterManager)
        {
            this._filterManager = filterManager;
        }

        /*
        // GET: api/Filter
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // GET: api/Filter/5
        //[HttpGet]
        //[Route("/filter")]
        public IHttpActionResult Get([FromUri]ModelView model)
        {
            if (model.skip == null) model.skip = 1;
            if (model.take == null) model.take = 20;
            if (model.type == null) model.type = "text";

            return Ok(this._filterManager.step3(model.type, model.csvUri, model.skip, model.take));
        }

        [HttpPost]
        // POST: api/Filter
        public IHttpActionResult Post([FromUri]ModelView model)
        {
            if (model.skip == null) model.skip = 1;
            if (model.take == null) model.take = 20;

            List<Values> vals = this._filterManager.step2(model.csvUri)
                .Skip(model.skip.Value)
                .Take(model.take.Value)
                .ToList();
            return Ok(vals);
        }

        // PUT: api/Filter/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Filter/5
        public void Delete(int id)
        {
        }
    }
}
