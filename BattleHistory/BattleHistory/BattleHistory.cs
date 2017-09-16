using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleHistory
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [ExportMetadata("Guid", "7573ADBE-68BC-4CF4-97EF-BE9D8933B903")]
    [ExportMetadata("Title", "")]
    [ExportMetadata("Description", "")]
    [ExportMetadata("Version", "")]
    [ExportMetadata("Author", "")]
    class BattleHistory : IPlugin, ITool
    {
        string ITool.Name => string.Empty;

        object ITool.View => new UserControl1();

        public void Initialize()
        {
            //throw new NotImplementedException();
        }
    }
}
