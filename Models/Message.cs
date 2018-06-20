using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserDashboard.Models{
    public class Message{
        public int MessageId {get;set;}
        public string message {get;set;}
        public int MessageSentId {get;set;}
        public User MessageSent {get;set;}
        public int MessageReceivedId {get;set;}
        public User MessageReceived {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        List<Comment> comments {get;set;}
        public Message(){
            comments = new List<Comment>();
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
        public string duration{
            get{
                double days =DateTime.Now.Subtract(this.created_at).TotalDays;
                double hours =DateTime.Now.Subtract(this.created_at).TotalHours;
                double minutes = DateTime.Now.Subtract(this.created_at).TotalMinutes;
                if((int)days > 27){
                    return $"{this.created_at.ToString("MMM d yyyy")}";
                }
                if((int)hours > 24){
                    return $"{(int)days} days ago";
                }
                else if((int)minutes > 60){
                    return $"{(int)hours} hours ago";
                }
                else{
                    return $"{(int)minutes} minutes ago";
                }
            }
        }

    }
}