using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserDashboard.Models{
    public class User{
        public int UserId {get;set;}
        public string firstname {get;set;}
        public string lastname {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public int level {get;set;}
        public string description {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        [InverseProperty("MessageRecevied")]
        List<Message> MessagesReceived {get;set;}
        [InverseProperty("MessageSent")]
        List<Message> MessagesSent {get;set;}
        List<Comment> comments {get;set;}
        public User(){
            MessagesSent = new List<Message>();
            comments = new List<Comment>();
            MessagesReceived = new List<Message>();
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
        public string duration{
            get{
                double days =DateTime.Now.Subtract(this.created_at).TotalDays;
                double hours =DateTime.Now.Subtract(this.created_at).TotalHours;
                double minutes = DateTime.Now.Subtract(this.created_at).TotalMinutes;
                if((int)hours > 24){
                    return $"{(int)days}";
                }
                else if((int)minutes > 60){
                    return $"{(int)hours}";
                }
                else{
                    return $"{(int)minutes}";
                }
            }
        }
    }
}