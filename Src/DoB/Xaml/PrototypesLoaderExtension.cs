using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;


namespace DoB.Xaml
{
	public class PrototypesLoaderExtension: MarkupExtension
	{
		public string FileNames 
		{
			/*get
			{
                List<Prototypes> result = new List<Prototypes>();
                foreach (var fileName in FileNames.Split(';'))
                {
                    result.Add(Prototypes.LoadFrom("StageData\\" + fileName + ".xaml"));
                }
                return FileNames;
            }
			set
			{ 
				FileNames = value;  
			}*/
			get; set;
		}

		public PrototypesLoaderExtension()
		{
			Debug.WriteLine("[i] PrototypesLoaderExtension");
		}

		public PrototypesLoaderExtension(string fileNames)
		{
			FileNames = fileNames;
		}

		public /*override*/ object ProvideValue(string NamesArray)//( IServiceProvider serviceProvider )
		{
			var result = new List<Prototypes>();

            //foreach( var fileName in FileNames.Split(';') )
            foreach (var fileName in NamesArray.Split(';'))
            {
				result.Add(Prototypes.LoadFrom( "StageData\\" + fileName + ".xaml" ));
			}
			return result;
		}
	}
}
