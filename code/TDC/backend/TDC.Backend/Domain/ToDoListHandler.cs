﻿using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using TDC.Backend.DataRepository;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class ToDoListHandler(
        IListRepository listRepository,
        IListItemRepository listItemRepository,
        IListMemberRepository listMemberRepository,
        IListInvitationRepository listInvitationRepository,
        IListRewardingRepository listRewardingRepository,
        IOpenRewardsRepository openRewardsRepository)
        : IToDoListHandler
    {
        public readonly IListRepository _listRepository = listRepository;
        public readonly IListItemRepository _listItemRepository = listItemRepository;
        public readonly IListMemberRepository _listMemberRepository = listMemberRepository;
        public readonly IListInvitationRepository _listInvitationRepository = listInvitationRepository;
        public readonly IListRewardingRepository _listRewardingRepository = listRewardingRepository;
        public readonly IOpenRewardsRepository _openRewardsRepository = openRewardsRepository;

        public long CreateList(string creator, ToDoListSavingDto newList)
        {
            var listDbo = new ToDoListDbo(0, newList.Name, newList.IsCollaborative, false);
            var listId = _listRepository.CreateList(listDbo);
            _listMemberRepository.AddListMember(listId, creator, true);
            return listId;
        }

        public Task AddUserToList(long listId, string username)
        {
            if(!ListExists(listId)) { return Task.CompletedTask;}
            if (UserIsListMember(listId, username)) { return Task.CompletedTask; }
            if (!ListIsCollaborative(listId)) { return Task.CompletedTask; }
            _listMemberRepository.AddListMember(listId, username, false);
            return Task.CompletedTask;
        }

        public Task RemoveUserFromList(long listId, string username)
        {
            if (!UserIsListMember(listId,username)) { return Task.CompletedTask; }
            if (_listMemberRepository.UserIsCreator(listId, username)) { return Task.CompletedTask; }
            _listMemberRepository.RemoveListMember(listId, username);
            return Task.CompletedTask;
        }

        public Task CancelListInvitation(long listId, string fromUser, string forUser)
        {
            _listInvitationRepository.DeleteListInvitation(forUser, fromUser, listId);
            return Task.CompletedTask;
        }

        public Task SendListInvitation(long listId, string fromUser, string forUser)
        {
            var invitations = _listInvitationRepository.GetInvitationsForUser(forUser);
            if (invitations.Any(invitation => invitation.FromUser.Equals(fromUser) && invitation.ListId == listId))
            {
                return Task.CompletedTask;
            }
            _listInvitationRepository.AddListInvitation(forUser, fromUser, listId);
            return Task.CompletedTask;
        }

        public Task DenyListInvitation(long listId, string decliningUser)
        {
            var invitations = _listInvitationRepository.GetInvitationsForUser(decliningUser);

            foreach (var invitation in invitations) {
                if (invitation.ListId == listId) { 
                    _listInvitationRepository.DeleteListInvitation(decliningUser, invitation.FromUser, invitation.ListId);
                }
            }
            return Task.CompletedTask;
        }

        public Task AcceptListInvitation(long listId, string newUser)
        {
            AddUserToList(listId, newUser);
            var invitations = _listInvitationRepository.GetInvitationsForUser(newUser);

            foreach (var invitation in invitations)
            {
                if (invitation.ListId == listId)
                {
                    _listInvitationRepository.DeleteListInvitation(newUser, invitation.FromUser, invitation.ListId);
                }
            }
            return Task.CompletedTask;
        }

        public List<ListInvitationDto> LoadListInvitationsForUser(string username)
        {
            return _listInvitationRepository
                .GetInvitationsForUser(username)
                .Select(dbo => new ListInvitationDto(dbo.FromUser, dbo.ListId))
                .ToList();
        }

        public Task DeleteList(long listId, string sender)
        {
            if (!UserIsCreator(listId, sender)) { 
                _listMemberRepository.RemoveListMember(listId, sender);
                return Task.CompletedTask; 
            }
            _listRepository.DeleteList(listId);
            return Task.CompletedTask;
        }

        public Task FinishList(long listId, string sender)
        {
            var members = _listMemberRepository.GetListMembers(listId);
            if (!members.Contains(sender)) { return Task.CompletedTask; }
            if (!ListCanBeFinished(listId)) { return Task.CompletedTask; }

            GrantListRewards(listId, members);
            
            _listRepository.FinishList(listId);
            return Task.CompletedTask;
        }

        public List<ToDoListLoadingDto> GetListsForUser(string username)
        {
            var listIds = _listMemberRepository.GetListsForUser(username);
            var listDboList = listIds.Select(listId => _listRepository.GetById(listId)).OfType<ToDoListDbo>().ToList();

            var listDtoList = new List<ToDoListLoadingDto>();
            foreach (var listDbo in listDboList)
            {
                if (listDbo.IsFinished) { continue; }

                listDtoList.Add(new ToDoListLoadingDto(listDbo.Id, listDbo.Name, listDbo.IsCollaborative));
            }
            return listDtoList;
        }

        public ToDoListLoadingDto? GetListById(long listId)
        {
            var dbo = _listRepository.GetById(listId);
            return dbo == null ? null : new ToDoListLoadingDto(dbo.Id, dbo.Name, dbo.IsCollaborative);
        }

        #region list items

        public long AddItemToList(long listId, string itemDescription, int itemEffort)
        {
            if(!ListExists(listId)) { return -1;}
            var itemId = _listItemRepository.AddItemToList(new ToDoListItemDbo(0, listId, itemDescription, itemEffort));
            return itemId;
        }

        public Task DeleteItem(long itemId)
        {
            _listItemRepository.DeleteItem(itemId);
            return Task.CompletedTask;
        }

        public Task UpdateItemDescription(long itemId, string description)
        {
            _listItemRepository.UpdateItemDescription(itemId, description);
            return Task.CompletedTask;
        }

        public Task UpdateItemEffort(long itemId, int effort)
        {
            _listItemRepository.UpdateItemEffort(itemId, effort);
            return Task.CompletedTask;
        }

        public Task UpdateListTitle(long listId, string newTitle)
        {
            _listRepository.UpdateListTitle(listId, newTitle);
            return Task.CompletedTask;
        }

        public Task SetItemStatus(long itemId, string updateForUser, bool isDone)
        {
            _listItemRepository.SetItemStatus(itemId, updateForUser, isDone);
            return Task.CompletedTask;
        }

        public List<ToDoListItemLoadingDto> GetItemsForList(long listId, string username)
        {
            var dbos = _listItemRepository.GetItemsForList(listId);
            var members = _listMemberRepository.GetListMembers(listId);
            var dtos = new List<ToDoListItemLoadingDto>();
            foreach (var dbo in dbos) {
                var dto = ParseItemDboToDto(dbo, username, members);
                dtos.Add(dto);
            }
            return dtos;
        }

        #endregion

        #region list rewarding
        public List<RewardingMessageDto> GetOpenRewardsForUser(string username)
        {
            var ids = _openRewardsRepository.GetOpenRewardsForUser(username);
            var dtos = new List<RewardingMessageDto>();
            foreach (var id in ids) {
                var dtoMessage = _listRewardingRepository.GetRewardingById(id);

                if (dtoMessage != null)
                {
                    dtos.Add(new RewardingMessageDto(dtoMessage, id));
                }
            }
            return dtos;
        }

        public void RemoveSeenRewardingForUser(string username, long listId)
        {
            _openRewardsRepository.RemoveSeenReward(username, listId);
        }

        public ListMembersDto GetMembersForList(long listId)
        {
            var members = _listMemberRepository.GetListMembers(listId);
            return new ListMembersDto(members);
        }

        public int GetPointsForMember(string username, long listId)
        {
            var points = 0;
            var items = _listItemRepository.GetItemsForList(listId);
            foreach (var item in items)
            {
                var isDone = _listItemRepository.GetItemStatus(item.Id, username);
                if (isDone)
                {
                    points += item.Effort * 5;
                }
            }
            return points;
        }
        #endregion

        #region privates
        private void GrantListRewards(long listId, List<string> listMembers)
        {
            var memberPoints = GetMemberPointValues(listMembers, listId);
            var memberPlacements = GetMemberPlacement(memberPoints);

            var message = GetRewardingMessage(listMembers, memberPoints, memberPlacements);

            _listRewardingRepository.AddNewRewarding(listId, message);

            foreach (var member in listMembers) {
                _openRewardsRepository.AddOpenRewardForUser(member, listId);
            }
        }

        private string GetRewardingMessage(List<string> members, Dictionary<string, int> memberPoints, Dictionary<string, uint> memberPlacements)
        {
            var message = string.Empty;
            for(var i = 0; i < members.Count; i++)
            {
                message += members[i] + ";";
                message += memberPoints.GetValueOrDefault(members[i]) + ";";
                message += memberPlacements.GetValueOrDefault(members[i]);

                if(i < members.Count - 1)
                {
                    message += System.Environment.NewLine;
                }
            }
            
            return message;
        }

        private Dictionary<string, int> GetMemberPointValues(List<string> listMembers, long listId)
        {
            var memberPoints = new Dictionary<string, int>();
            foreach (var member in listMembers)
            {
                memberPoints.Add(member, GetPointsForMember(member, listId));
            }
            return memberPoints;
        }

        private Dictionary<string, uint> GetMemberPlacement(Dictionary<string, int> memberPoints)
        {
            var memberPlacement = new Dictionary<string, uint>();
            var sorted = memberPoints.OrderByDescending(kvp => kvp.Value).ThenBy(kvp => kvp.Key).ToList();

            uint currentPlace = 0;
            uint actualPlace = 0;
            int? previousPoints = null;

            foreach (var kvp in sorted)
            {
                actualPlace++;

                if (previousPoints == null || kvp.Value != previousPoints) { currentPlace = actualPlace; }

                memberPlacement[kvp.Key] = currentPlace;
                previousPoints = (int?)kvp.Value;
            }

            return memberPlacement;
        }

        private ToDoListItemLoadingDto ParseItemDboToDto(ToDoListItemDbo dbo, string currentUser, List<string> listMembers)
        {
            var isDone = _listItemRepository.GetItemStatus(dbo.Id, currentUser);
            var finishedMembers = listMembers
                .Where(member => _listItemRepository.GetItemStatus(dbo.Id, member) && !member.Equals(currentUser))
                .ToList();
            return new ToDoListItemLoadingDto(dbo.Id, dbo.Description, isDone, finishedMembers, dbo.Effort);
        }

        private bool ListCanBeFinished(long listId)
        {
            var listItems = _listItemRepository.GetItemsForList(listId);
            return listItems.All(listItem => AnyoneHasFinished(listId, listItem.Id));
        }

        private bool AnyoneHasFinished(long listId, long itemId)
        {
            var listMembers = _listMemberRepository.GetListMembers(listId);
            return listMembers.Any(member => _listItemRepository.GetItemStatus(itemId, member) == true);
        }

        private bool UserIsListMember(long listId, string username) {
            var members = _listMemberRepository.GetListMembers(listId);
            return members.Any(member => member.Equals(username));
        }

        private bool ListIsCollaborative(long listId) {
            var list = _listRepository.GetById(listId)!;
            return list.IsCollaborative;
        }

        private bool UserIsCreator(long listId, string username)
        {
            return _listMemberRepository.UserIsCreator(listId, username);
        }

        private bool ListExists(long listId)
        {
            return _listRepository.GetById(listId) != null;
        }

        #endregion
    }
}
