// (C) Copyright 2002-2005 by Autodesk, Inc. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to 
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

[assembly: CommandClass(typeof(ClassLibrary.arxClass))]

namespace ClassLibrary
{
    /// <summary>
    /// Summary description for arxClass.
    /// </summary>
    public class arxClass
    {
        public arxClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // Define Command "AsdkCmd1"
        [CommandMethod("AsdkCmd1")]
        static public void test() // This method can have any name
        {
            // Put your command code here
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string str = "Second";
            ed.WriteMessage(str); 
        }

    }
}