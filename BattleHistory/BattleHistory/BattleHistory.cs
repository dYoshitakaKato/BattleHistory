using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using MetroTrilithon.Lifetime;
using MetroTrilithon.Mvvm;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Nekoxy;
using Grabacr07.KanColleWrapper.Models.Raw;
using BattleHistory.Models;

namespace BattleHistory
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [ExportMetadata("Guid", "7573ADBE-68BC-4CF4-97EF-BE9D8933B903")]
    [ExportMetadata("Title", "BattleHistory")]
    [ExportMetadata("Description", "")]
    [ExportMetadata("Version", "0.0")]
    [ExportMetadata("Author", "日和")]
    class BattleHistory : IPlugin, ITool, IDisposableHolder
    {
        private bool initialized;

        string ITool.Name => "BattleHistory";

        object ITool.View => new UserControl1();

        private readonly MultipleDisposable compositDisposable = new MultipleDisposable();
        private readonly List<IDisposable> fleetHandlers = new List<IDisposable>();
        public void Dispose() => this.compositDisposable.Dispose();
        ICollection<IDisposable> IDisposableHolder.CompositeDisposable => this.compositDisposable;

        public void Initialize()
        {
            KanColleClient.Current
                .Subscribe(nameof(KanColleClient.IsStarted), () => InitializeCore(), false)
                .AddTo(this);
        }

        public void InitializeCore()
        {
            var homeport = KanColleClient.Current.Homeport;
            //var test = KanColleClient.Current.Proxy.api_req_combined_battle_battleresult;
            var result = new Grabacr07.KanColleWrapper.Models.Raw.kcsapi_combined_battle();
            var map = new Grabacr07.KanColleWrapper.Models.Raw.kcsapi_map_start();
            var proxy = KanColleClient.Current.Proxy;

            if (homeport == null) return;
            this.initialized = true;
        }

        private void SetDisposable(KanColleProxy proxy)
        {
            SetMapDisposable(proxy);
            SetBattleResultDisposable(proxy);
        }

        private void SetMapDisposable(KanColleProxy proxy)
        {
            var mapStart = proxy.api_req_map_start.TryParse<kcsapi_map_start>().Subscribe(x => Update(x.Data));
            compositDisposable.Add(mapStart);

            var next = KanColleClient.Current.Proxy.ApiSessionSource.Where
                (x => x.Request.PathAndQuery == "/kcsapi/api_req_map/next").TryParse<kcsapi_map_start>().Subscribe(x => Update(x.Data));
            compositDisposable.Add(next);
        }

        private void SetBattleResultDisposable(KanColleProxy proxy)
        {
            var comBatResult = proxy.api_req_combined_battle_battleresult.TryParse<kcsapi_battleresult>().Subscribe(x => Update(x.Data));
            var batResult = proxy.api_req_sortie_battleresult.TryParse<kcsapi_battleresult>().Subscribe(x => Update(x.Data));

            compositDisposable.Add(comBatResult);
            compositDisposable.Add(batResult);
        }

        private void Update(kcsapi_map_start data)
        {
            Console.WriteLine("kcsapi_map_start");
        }

        BattleResult btResult;

        private void Update(kcsapi_battleresult data)
        {
            btResult = new BattleResult(data);
        }
    }
}
