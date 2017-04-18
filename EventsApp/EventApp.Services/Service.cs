using EventsApp.Data;

namespace EventApp.Services
{
   public abstract class Service
    {
        private EventsAppContext context;

        public  EventsAppContext Context => context ?? (context = new EventsAppContext());
        
    }
}
