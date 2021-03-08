using Abp.Dependency;
using Abp.Webhooks;
using Domain.EventTypes;
using Domain.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Implementations
{
    public class FixedEventTypesRepository: IEventTypesRepository
    {
        private static List<Guid> _fixedGuids = new List<Guid>()
        {
            Guid.Parse("0E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("1E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("2E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("3E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("4E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("5E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("6E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("7E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("8E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("9E4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("AE4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("BE4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("CE4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("DE4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("EE4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
            Guid.Parse("FE4ABCAA-4DC0-493F-8EE1-21000B870FFD"),
        };

        public FixedEventTypesRepository()
        {
            if (_fixedList == null)
            {
                _fixedList = new List<EventType>();
                var definitionMngr = IocManager.Instance.Resolve<IWebhookDefinitionManager>();
                var cnt = 0;
                foreach (var definition in definitionMngr.GetAll())
                {
                    _fixedList.Add(new EventType(_fixedGuids[cnt], definition.Name));
                    cnt++;
                }
            }
        }
        private static List<EventType> _fixedList = null;
        
        public Maybe<EventType> Get(EventTypeId eventTypeId)
        {
            var result = _fixedList.Where(e => e.Id.Equals(eventTypeId));

            if (result.Count() == 0)
            {
                return new Maybe<EventType>();
            }

            return new Maybe<EventType>(result.First());
        }

        public IEnumerable<EventType> GetAll()
        {
            return _fixedList;
        }
    }
}
