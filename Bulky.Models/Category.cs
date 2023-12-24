using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        //* [Key] //Primary key -- If the Id is named something else
        // if the Name CategoryId it alse  it will automatically treat that partivaular property as the primary key

        //*/ 
        public int Id { get; set; }    //Primary ket of the table
                                       //Default,If the name is purely ID, it will automatically or rather entity framework cor will automatically think this is the primary  key
        [Required] // once you add required to any of the property here when SQL script will be generated to create this particular table in datebase , You will see that string have a not null setting
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
