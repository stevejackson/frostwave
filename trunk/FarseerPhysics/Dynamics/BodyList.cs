using System;
using System.Collections.Generic;
using System.Text;

namespace FarseerGames.FarseerPhysics.Dynamics {
    public class BodyList : List<Body> {
        private List<Body> markedForRemovalList = new List<Body>();
        
        public delegate void BodyAddedRemovedDelegate(Body body);
        public BodyAddedRemovedDelegate BodyAddedHandler;
        public BodyAddedRemovedDelegate BodyRemovedHandler;

        public new void Add(Body body) {
            base.Add(body);
            if (BodyAddedHandler != null) { BodyAddedHandler(body); }
        }

        public new void Remove(Body body) {
            base.Remove(body);
            if (BodyRemovedHandler != null) { BodyRemovedHandler(body); }
        }

        public void RemoveDisposed() {
            for (int i = 0; i < Count; i++) {
                if (IsDisposed(this[i])) {
                    markedForRemovalList.Add(this[i]);
                }
            }
            for (int j = 0; j < markedForRemovalList.Count; j++) {
                this.Remove(markedForRemovalList[j]);
            }
            markedForRemovalList.Clear();
        }

        

        internal static bool IsDisposed(Body a) {
            return a.IsDisposed;
        }
    }
}
