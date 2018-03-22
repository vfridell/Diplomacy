﻿using DiplomacyLib.Models;
using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnEdge : EdgeBase<DrawnMapNode>
    {
        public DrawnEdge(DrawnMapNode source, DrawnMapNode target)
            :base(source, target)
        {
            ID = (source.MapNode.SequenceNumber * 1000) + target.MapNode.SequenceNumber;
        }

        public override bool Equals(object obj)
        {
            var otherEdge = obj as DrawnEdge;
            if (null == otherEdge) return false;
            return Equals(otherEdge);
        }

        public bool Equals(DrawnEdge other)
        {
            return ID == other.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Source} <-> {Target}";
        }

    }
}
