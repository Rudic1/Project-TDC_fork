using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class SetItemStatusTests
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
        public void SetItemStatus_CallsRepository()
        {
            _target.SetItemStatus(1, "test-user", true);
            _target._listItemRepository.Received().SetItemStatus(1, "test-user", true);
        }
    }
}
