using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation.Scaffold
{
    public interface IBuildScaffolds
    {
        Scaffolding Scaffold(Type type, TranspileOptions options, OutputLanguage outputLangauge);
    }
}
