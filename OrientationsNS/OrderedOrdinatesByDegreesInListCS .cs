using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Rover3.OrientationsNS
{
    static class OrderedOrdinatesByDegreesInListCS
    {
       public static IList<Orientation> orderedOrdinatesByDegreesInList = new List<Orientation>();


        //don't need dictionary just look through list! or array!
       static OrderedOrdinatesByDegreesInListCS()
        {


            //is this reflection better https://youtu.be/nqAHJmpWLBg?t=972 
            var orientations = Assembly.GetAssembly(typeof(Orientation)).GetTypes()
                .Where(orientation => orientation.IsClass && !orientation.IsAbstract && orientation.IsSubclassOf(typeof(Orientation)) && (orientation.Namespace == "OrientationsNS"));

            


            //can i remove var
            //shouldnt name the var the same name!
            foreach (var orientation in orientations)
            {
                Orientation orientationInst = Activator.CreateInstance(orientation) as Orientation;
                orderedOrdinatesByDegreesInList.Add(orientationInst);
            }
            orderedOrdinatesByDegreesInList.OrderBy(orientationObj => orientationObj.compassDegrees);
            


        }
    }

}






        

