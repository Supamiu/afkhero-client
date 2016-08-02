using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Common
{
    /// <summary>
    /// Parallax scrolling script that should be assigned to a layer
    /// </summary>
    public class ScrollingScript : MonoBehaviour
    {
        public bool moving = true;

        public float baseSpeed = 2f;

        /// <summary>
        /// Moving direction
        /// </summary>
        public Vector2 direction = new Vector2(-1, 0);

        /// <summary>
        /// Movement should be applied to camera
        /// </summary>
        public bool isLinkedToCamera = false;

        /// <summary>
        /// 1 - Background is infinite
        /// </summary>
        public bool isLooping = false;

        public float speedBonus = 1f;

        /// <summary>
        /// 2 - List of children with a renderer.
        /// </summary>
        private List<Transform> backgroundPart;

        public float Speed
        {
            get
            {
                return baseSpeed * speedBonus;
            }
        }

        // 3 - Get all the children
        void Awake()
        {
            EventDispatcher.Instance.Register("movespeed.bonus", new Listener<GenericGameEvent<float>>((ref GenericGameEvent<float> e) =>
            {
                speedBonus += e.Data;
            }));

            EventDispatcher.Instance.Register("movement.enabled", new Listener<GenericGameEvent<bool>>((ref GenericGameEvent<bool> e) =>
            {
                moving = e.Data;
            }));

            // For infinite background only
            if (isLooping)
            {
                // Get all the children of the layer with a renderer
                backgroundPart = new List<Transform>();

                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);

                    // Add only the visible children
                    if (child.GetComponent<Renderer>() != null)
                    {
                        backgroundPart.Add(child);
                    }
                }

                // Sort by position.
                // Note: Get the children from left to right.
                // We would need to add a few conditions to handle
                // all the possible scrolling directions.
                backgroundPart = backgroundPart.OrderBy(
                    t => t.position.x
                ).ToList();
            }
        }

        void Update()
        {
            float x = baseSpeed * speedBonus;

            if (moving)
            {
                // Movement
                Vector3 movement = new Vector3(
                                      x * direction.x,
                                      0,
                                      0);

                movement *= Time.deltaTime;
                transform.Translate(movement);

                // Move the camera
                if (isLinkedToCamera)
                {
                    Camera.main.transform.Translate(movement);
                }

                // 4 - Loop
                if (isLooping)
                {
                    // Get the first object.
                    // The list is ordered from left (x position) to right.
                    Transform firstChild = backgroundPart.FirstOrDefault();

                    if (firstChild != null)
                    {
                        // Check if the child is already (partly) before the camera.
                        // We test the position first because the IsVisibleFrom
                        // method is a bit heavier to execute.
                        if (firstChild.position.x < Camera.main.transform.position.x)
                        {
                            // If the child is already on the left of the camera,
                            // we test if it's completely outside and needs to be
                            // recycled.
                            if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                            {
                                // Get the last child position.
                                Transform lastChild = backgroundPart.LastOrDefault();
                                Vector3 lastPosition = lastChild.transform.position;
                                Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                                // Set the position of the recyled one to be AFTER
                                // the last child.
                                // Note: Only work for horizontal scrolling currently.
                                firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                                // Set the recycled child to the last position
                                // of the backgroundPart list.
                                backgroundPart.Remove(firstChild);
                                backgroundPart.Add(firstChild);
                            }
                        }
                    }
                }
            }
        }
    }
}
