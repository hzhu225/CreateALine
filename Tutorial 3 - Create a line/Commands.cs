// Written by Felix Zhu @ CMS Surveyors

/********************************************************************************************************************************************
     In this tutorial, you will have the same codes as in Tutorial 2. Explanation of codes will be provided.
     You don't need to write any code in this tutorial.
*********************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;

namespace Tutorial_3
{
    public class Commands
    {
        [CommandMethod("Tut3")]
        public void Tutorial_3_Create_a_line()
        {
            CivilDocument doc = CivilApplication.ActiveDocument;
            Document MdiActdoc = Application.DocumentManager.MdiActiveDocument;
            Editor editor = MdiActdoc.Editor;
            Database currentDB = MdiActdoc.Database;

            using (Transaction trans = HostApplicationServices.WorkingDatabase.TransactionManager.StartOpenCloseTransaction())
            {
                BlockTable blockTab = trans.GetObject(currentDB.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord modelSpaceBTR = trans.GetObject(blockTab[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                //Codes start from here:

                //You might noticed some text are in black, while others such as Transaction and BlockTable are in cyan.

                //You can call things in cyan color "type", but they are actually class (or struct) defined by Autodesk.
                //We can use them because we have "Using Directive" above (Mentioned in Tutorial 1), which means this is linked to some files which have class definitions. 

                //Both class and struct are "frameworks". They are in "void form". They have no "body" and cannot exist in database.

                //If you want to create a "real object" (instance) of a class, you need to use its constructor and fill in parameters it needs. (e.g just like fill concrete into framework)
                //This process is called instantiation. It actually acquires a space in your RAM to store this object. 

                //An object can be called "an instance of a class"
                //Point3d has a constructor, which takes 3 double type values -- X, Y and Z.
                //So we can create two instances of Point3d as follow:


                Point3d pt1 = new Point3d(0, 0, 0);
                Point3d pt2 = new Point3d(5, 5, 5);


                //Line has a constructor which takes 2 Point3d values, so we can create an instance of Line as follow:

                Line myline = new Line(pt1, pt2);


                //IMPORTANT: Civil3D database is not equivalent to modelSpace. Although pt1,pt2 and myline are all in database now, you cannot see them in modelSpace yet.
                //Codes below is to add "myline" to modelSpace and transaction. For every object you want to add to modelSpace, you need to use them.

                modelSpaceBTR.AppendEntity(myline);
                trans.AddNewlyCreatedDBObject(myline, true);

                //Not every "type" could be added to modelSpace. For example, Point3d cannot be added to modelSpace, it can only exist in database. 
                //Why? Because Point3d is a valueType, it is only used to store values and not drawable in modelSpace. 
                //Think about a number, you can have a number stored in database, but you cannot show it directly in modelSpace.
                //If you want to see the position of Point3d, you need to create an instance of DBPoint (AutoCAD Point) or CogoPoint, and use Point3d as position parameter in its constructor.


                trans.Commit();         //Commit the transaction. Now you are able to see a line created in Civil3D modelSpace.
            }
        }
    }
}



//Apparently, if you build this code and load dll file in Civil3D, you can use "Tut3" command, which does exactly the same thing as "Tut2" Command.
//So what are these green comments do? Nothing. When you building the code, the compiler will escape all comments.

