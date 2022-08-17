/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using Oculus.Interaction.Input;
using UnityEngine;

namespace Oculus.Interaction.GrabAPI
{
    public interface IFingerAPI
    {
        bool GetFingerIsGrabbing(HandFinger finger);
        bool GetFingerIsGrabbingChanged(HandFinger finger, bool targetPinchState);
        float GetFingerGrabStrength(HandFinger finger);
        Vector3 GetCenterOffset();
        void Update(IHand hand);
    }
}