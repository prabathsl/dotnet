using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class JaliyasBlogDataSource : DataSourceBase<RssSchema>
    {
        private const string _url =@"http://jaliyaudagedara.blogspot.com/feeds/posts/default";

        protected override string CacheKey
        {
            get { return "JaliyasBlogDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<RssSchema>> LoadDataAsync()
        {
            try
            {
                var rssDataProvider = new RssDataProvider(_url);
                return await rssDataProvider.Load();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("JaliyasBlogDataSourceDataSource.LoadData", ex.ToString());
                return new RssSchema[0];
            }
        }
    }
}
