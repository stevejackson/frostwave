using System;
using System.Collections.Generic;
using System.Text;

namespace FarseerGames.FarseerPhysics.Dynamics {
    public class ControllerList : List<Controller> {
        private List<Controller> markedForRemovalList = new List<Controller>();

        public delegate void ControllerAddedRemovedDelegate(Controller controller);
        public ControllerAddedRemovedDelegate ControllerAddedHandler;
        public ControllerAddedRemovedDelegate ControllerRemovedHandler;

        public new void Add(Controller controller) {
            base.Add(controller);
            if (ControllerAddedHandler != null) { ControllerAddedHandler(controller); }
        }

        public new void Remove(Controller controller) {
            base.Remove(controller);
            if (ControllerRemovedHandler != null) { ControllerRemovedHandler(controller); }
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

        internal static bool IsDisposed(Controller controller) {
            return controller.IsDisposed;
        }
    }
}
