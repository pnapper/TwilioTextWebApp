using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Texter.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ApplicationUser User { get; set; }

        public override bool Equals(System.Object otherContact)
        {
            if (!(otherContact is Contact))
            {
                return false;
            }
            else
            {
                Contact newItem = (Contact)otherContact;
                return this.ContactId.Equals(newItem.ContactId);
            }
        }

        public override int GetHashCode()
        {
            return this.ContactId.GetHashCode();
        }
    }
}