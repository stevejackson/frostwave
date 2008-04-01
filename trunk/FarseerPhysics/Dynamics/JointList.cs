using System;
using System.Collections.Generic;
using System.Text;

namespace FarseerGames.FarseerPhysics.Dynamics {
    public class JointList : List<Joint> {
        private List<Joint> markedForRemovalList = new List<Joint>();

        public delegate void JointAddedRemovedDelegate(Joint joint);
        public JointAddedRemovedDelegate JointAddedHandler;
        public JointAddedRemovedDelegate JointRemovedHandler;

        public new void Add(Joint joint) {
            base.Add(joint);
            if (JointAddedHandler != null) { JointAddedHandler(joint); }
        }

        public new void Remove(Joint joint) {
            base.Remove(joint);
            if (JointRemovedHandler != null) { JointRemovedHandler(joint); }
        }

        public void RemoveDisposed() {
            for (int i = 0; i < Count; i++) {
                if (this[i].IsDisposed) {
                    markedForRemovalList.Add(this[i]);
                }
            }
            for (int j = 0; j < markedForRemovalList.Count; j++) {
                this.Remove(markedForRemovalList[j]);
            }
            markedForRemovalList.Clear();
        }
    }
}
