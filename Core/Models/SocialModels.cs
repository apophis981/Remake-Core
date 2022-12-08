﻿using System.Collections.Generic;

namespace SubterfugeCore.Models.GameEvents
{

    public interface INetworkSocialController
    {
        BlockPlayerResponse BlockPlayer(BlockPlayerRequest blockPlayerRequest);
        UnblockPlayerResponse UnblockPlayer(UnblockPlayerRequest unblockPlayerRequest);
        ViewBlockedPlayersResponse ViewBlockedPlayers(ViewBlockedPlayersRequest viewBlockedPlayersRequest);
        SendFriendRequestResponse SendFriendRequest(SendFriendRequestRequest sendFriendRequestRequest);
        AcceptFriendRequestResponse AcceptFriendRequest(AcceptFriendRequestRequest acceptFriendRequestRequest);
        DenyFriendRequestResponse DenyFriendRequest(DenyFriendRequestRequest denyFriendRequestRequest);
        ViewFriendRequestsResponse ViewFriendRequests(ViewFriendRequestsRequest viewFriendRequestsRequest);
        RemoveFriendResponse RemoveFriend(RemoveFriendRequest removeFriendRequest);
        ViewFriendsResponse ViewFriends(ViewFriendsRequest viewFriendsRequest);
    }
    
    public class BlockPlayerRequest
    {
        public string userIdToBlock { get; set; }
    }

    public class BlockPlayerResponse : NetworkResponse { }

    public class UnblockPlayerRequest
    {
        public string UserIdToUnblock { get; set; }
    }
    
    public class UnblockPlayerResponse : NetworkResponse { }

    public class ViewBlockedPlayersRequest { }

    public class ViewBlockedPlayersResponse : NetworkResponse
    {
        public List<User> BlockedUsers { get; set; }
    }
    
    public class SendFriendRequestRequest
    { 
        public string FriendId { get; set; }
    }

    public class SendFriendRequestResponse : NetworkResponse { }

    public class AcceptFriendRequestRequest
    {
        public string FriendId { get; set; }
    }

    public class AcceptFriendRequestResponse : NetworkResponse { }

    public class DenyFriendRequestRequest
    {
        public string FriendId { get; set; }
    }

    public class DenyFriendRequestResponse : NetworkResponse { }

    public class ViewFriendRequestsRequest { }

    public class ViewFriendRequestsResponse : NetworkResponse
    {
        public List<User> IncomingFriendRequests { get; set; }
    }

    public class RemoveFriendRequest
    {
        public string FriendId { get; set; }
    }

    public class RemoveFriendResponse : NetworkResponse { }

    public class ViewFriendsRequest { }

    public class ViewFriendsResponse : NetworkResponse
    {
        public List<User> Friends { get; set; }
    }
}