using System;

namespace UserDashboard.Models{
    public class Comment{
        public int CommentId {get;set;}
        public string comment {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public int MessageId {get;set;}
        public Message Message {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public Comment(){
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