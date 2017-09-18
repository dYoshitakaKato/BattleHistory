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
using BattleHistory.Repository;

namespace BattleHistory
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [ExportMetadata("Guid", "7573ADBE-68BC-4CF4-97EF-BE9D8933B903")]
    [ExportMetadata("Title", "BattleHistory")]
    [ExportMetadata("Description", "")]
    [ExportMetadata("Version", "0.0")]
    [ExportMetadata("Author", "日和")]
    class StartUp : IPlugin, ITool, IDisposableHolder
    {
        string ITool.Name => "BattleHistory";

        object ITool.View => new UserControl1();

        private readonly MultipleDisposable _CompositDisposable = new MultipleDisposable();
        private readonly List<IDisposable> _FleetHandlers = new List<IDisposable>();
        public void Dispose() => this._CompositDisposable.Dispose();
        ICollection<IDisposable> IDisposableHolder.CompositeDisposable => this._CompositDisposable;
        private BattleHistoryRepository _Repository = new BattleHistoryRepository();

        public void Initialize()
        {
            KanColleClient.Current
                .Subscribe(nameof(KanColleClient.IsStarted), () => Initialize(KanColleClient.Current.Homeport), false)
                .AddTo(this);
        }

        public void Initialize(Homeport homeport)
        {
            if (homeport == null) return;
            SetDisposable(KanColleClient.Current.Proxy);
        }

        private void SetDisposable(KanColleProxy proxy)
        {
            SetMapDisposable(proxy);
            SetBattleResultDisposable(proxy);
        }

        private void SetMapDisposable(KanColleProxy proxy)
        {
            var mapStart = proxy.api_req_map_start.TryParse<kcsapi_map_start>().Subscribe(x => Create(x.Data));
            _CompositDisposable.Add(mapStart);

            var next = KanColleClient.Current.Proxy.ApiSessionSource.Where
                (x => x.Request.PathAndQuery == "/kcsapi/api_req_map/next").TryParse<kcsapi_map_start>().Subscribe(x => Update(x.Data));
            _CompositDisposable.Add(next);
        }

        private void SetBattleResultDisposable(KanColleProxy proxy)
        {
            var comBatResult = proxy.api_req_combined_battle_battleresult.TryParse<kcsapi_battleresult>().Subscribe(x => Update(x.Data));
            var batResult = proxy.api_req_sortie_battleresult.TryParse<kcsapi_battleresult>().Subscribe(x => Update(x.Data));

            _CompositDisposable.Add(comBatResult);
            _CompositDisposable.Add(batResult);
        }

        Models.BattleHistory _BattleHistory;

        private void Create(kcsapi_map_start data)
        {
            _BattleHistory = new Models.BattleHistory(data);
        }

        private void Update(kcsapi_map_start data)
        {
            _BattleHistory.Update(data);
        }

        private void Update(kcsapi_battleresult data)
        {
            _BattleHistory.BattleResult = new BattleResult(data);
            _Repository.Insert(_BattleHistory);
        }
    }
}
