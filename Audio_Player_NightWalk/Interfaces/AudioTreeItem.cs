﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    public interface AudioTreeItem
    {
       void UpdateData(FullAudiFileInfo info);


        string GetFullPath();


    }
}
