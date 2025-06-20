using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class FridgeModelDTO
    {

        public Guid Id { get; set; }= Guid.NewGuid();

        public string Name { get; set; }

        public int Year { get; set; }
    }
}
