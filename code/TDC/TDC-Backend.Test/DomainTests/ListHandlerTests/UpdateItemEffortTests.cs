using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class UpdateItemEffortTests
    {
        private ToDoListHandler _target;
        private IListRepository _listRepository;
        private IListItemRepository _listItemRepository;
        private IListMemberRepository _listMemberRepository;

        [SetUp]
        public void SetUp()
        {
            _listRepository = Substitute.For<IListRepository>();
            _listItemRepository = Substitute.For<IListItemRepository>();
            _listMemberRepository = Substitute.For<IListMemberRepository>();
            _target = new ToDoListHandler(_listRepository, _listItemRepository, _listMemberRepository);
        }

        [Test]
        public void UpdateItemEffort_CallsRepository()
        {
            _target.UpdateItemEffort(1, 4);
            _target._listItemRepository.Received().UpdateItemEffort(1, 4);
        }
    }
}
