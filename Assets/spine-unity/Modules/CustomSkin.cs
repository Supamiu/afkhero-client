/******************************************************************************
 * Spine Runtimes Software License
 * Version 2.3
 * 
 * Copyright (c) 2013-2015, Esoteric Software
 * All rights reserved.
 * 
 * You are granted a perpetual, non-exclusive, non-sublicensable and
 * non-transferable license to use, install, execute and perform the Spine
 * Runtimes Software (the "Software") and derivative works solely for personal
 * or internal use. Without the written permission of Esoteric Software (see
 * Section 2 of the Spine Software License Agreement), you may not (a) modify,
 * translate, adapt or otherwise create derivative works, improvements of the
 * Software or develop new applications using the Software or (b) remove,
 * delete, alter or obscure any trademarks or any copyright, trademark, patent
 * or other intellectual property or proprietary rights notices on or in the
 * Software, including any copy thereof. Redistributions in binary or source
 * form must include this license and terms.
 * 
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL ESOTERIC SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/
using UnityEngine;

namespace Spine.Unity.Modules {
	public class CustomSkin : MonoBehaviour {

		[System.Serializable]
		public class SkinPair {
			/// <summary>SpineAttachment attachment path to help find the attachment.</summary>
			/// <remarks>This use of SpineAttachment generates an attachment path string that can only be used by SpineAttachment.GetAttachment.</remarks>
			[SpineAttachment(currentSkinOnly: false, returnAttachmentPath: true, dataField: "skinSource")]
			[UnityEngine.Serialization.FormerlySerializedAs("sourceAttachment")]
			public string sourceAttachmentPath;

			[SpineSlot]
			public string targetSlot;

			/// <summary>The name of the skin placeholder/skin dictionary entry this attachment should be associated with.</summary>
			/// <remarks>This name is used by the skin dictionary, used in the method Skin.AddAttachment as well as setting a slot attachment</remarks>
			[SpineAttachment(currentSkinOnly: true, placeholdersOnly: true)]
			public string targetAttachment;
		}

		#region Inspector
		public SkeletonDataAsset skinSource;

		[UnityEngine.Serialization.FormerlySerializedAs("skinning")]
		public SkinPair[] skinItems;

		public Skin customSkin;
		#endregion

	    private SkeletonRenderer skeletonRenderer;

	    private void Start () {
			skeletonRenderer = GetComponent<SkeletonRenderer>();
			var skeleton = skeletonRenderer.skeleton;

			customSkin = new Skin("CustomSkin");

			foreach (var pair in skinItems) {
				var attachment = SpineAttachment.GetAttachment(pair.sourceAttachmentPath, skinSource);
				customSkin.AddAttachment(skeleton.FindSlotIndex(pair.targetSlot), pair.targetAttachment, attachment);
			}

			// The custom skin does not need to be added to the skeleton data for it to work.
			// But it's useful for your script to keep a reference to it.
			skeleton.SetSkin(customSkin);
		}
	}

}
