using BlazorChat.Shared;
using Microsoft.AspNetCore.Identity;

namespace BlazorChat.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ChatMessage> ChatMessagesFromUsers { get; set; }
        public virtual ICollection<ChatMessage> ChatMessagesToUsers { get; set; }   

        public ApplicationUser() 
        {
            ChatMessagesFromUsers = new HashSet<ChatMessage>();
            ChatMessagesToUsers = new HashSet<ChatMessage>();
        }
    }
}