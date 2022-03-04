using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asp_net_timetable.App_Start.StartUp))]
namespace Asp_net_timetable.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}