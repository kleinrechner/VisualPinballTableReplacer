using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPinballTableReplacer.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameID { get; set; }

        //public int? EMUID { get; set; }

        [Required]
        [StringLength(200)]
        public string GameName { get; set; }

        [Required]
        [StringLength(250)]
        public string GameFileName { get; set; }

        [Required]
        [StringLength(200)]
        public string GameDisplay { get; set; }

        //public int? UseEmuDefaults { get; set; }

        //public int? Visible { get; set; }

        //[Column(TypeName = "text")]
        //public string Notes { get; set; }

        //public DateTime? DateAdded { get; set; }

        //public int? GameYear { get; set; }

        //[StringLength(100)]
        //public string ROM { get; set; }

        //[StringLength(200)]
        //public string Manufact { get; set; }

        //public int? NumPlayers { get; set; }

        //public int? ResolutionX { get; set; }

        //public int? ResolutionY { get; set; }

        //public int? OutputScreen { get; set; }

        //public int? ThemeColor { get; set; }

        //[StringLength(50)]
        //public string GameType { get; set; }

        //[StringLength(200)]
        //public string TAGS { get; set; }

        //[StringLength(200)]
        //public string Category { get; set; }

        //[StringLength(200)]
        //public string Author { get; set; }

        //[StringLength(200)]
        //public string LaunchCustomVar { get; set; }

        //[StringLength(50)]
        //public string GKeepDisplays { get; set; }

        //[StringLength(100)]
        //public string GameTheme { get; set; }

        //public int? GameRating { get; set; }

        //[Column(TypeName = "text")]
        //public string Special { get; set; }

        //public int? sysVolume { get; set; }

        //[StringLength(250)]
        //public string DOFStuff { get; set; }

        //[StringLength(100)]
        //public string MediaSearch { get; set; }

        //[StringLength(50)]
        //public string AudioChannels { get; set; }

        //[StringLength(100)]
        //public string CUSTOM2 { get; set; }

        //[StringLength(100)]
        //public string CUSTOM3 { get; set; }

        //[StringLength(100)]
        //public string GAMEVER { get; set; }

        //[StringLength(250)]
        //public string ALTEXE { get; set; }

        //[StringLength(100)]
        //public string IPDBNum { get; set; }

        //public DateTime? DateUpdated { get; set; }

        //public DateTime? DateFileUpdated { get; set; }

        //public int? AutoRecFlag { get; set; }

        //[StringLength(250)]
        //public string AltRunMode { get; set; }

        //[StringLength(1000)]
        //public string WebLinkURL { get; set; }

        //[StringLength(200)]
        //public string DesignedBy { get; set; }
    }
}
