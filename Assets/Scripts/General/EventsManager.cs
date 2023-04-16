using System.Collections.Generic;
using UnityEngine;

namespace Assets.EventsManager
{
    public enum InputType {MouseClick, Sensor }
    public enum OutputType {Animation, Audio, Servo }
    [System.Serializable]
    public class EventOnOnePage
    {
        [System.Serializable]
        public struct thisEvent
        {
            public int EventIdx;
            public GameObject ThisObject;
            public GameObject thisUI;
            public List<InputType> thisInputs;
            public List<OutputType> thisOutputs;
        }
        public GameObject thisPage;
        public List<thisEvent> EventsOnThisPage=new List<thisEvent>();
    }

    public class EventsManager : MonoBehaviour
    {
        public List<EventOnOnePage> allEvents;
        public int currentPageIdx;
        public int currentEventIdx = 0;
        public GameObject eventPrefeb;
        public Transform eventContent;

        public void AddEvent()
        {
            if (!GameManager.Instance.currentObjectValidationCheck())
            {
                return;
            }
            //Get the EventList for this page
            currentPageIdx = GameManager.Instance.pageIdx;
            List<EventOnOnePage.thisEvent> EventList = allEvents[currentPageIdx].EventsOnThisPage;
            allEvents[GameManager.Instance.pageIdx].thisPage = GameManager.Instance.currentPage;
            EventOnOnePage.thisEvent newEvent = new EventOnOnePage.thisEvent();
            newEvent.ThisObject = GameManager.Instance.currentObjectInWindow.gameObject;
            newEvent.thisUI = Instantiate(eventPrefeb, eventContent);
            newEvent.EventIdx = EventList.Count + 1;
            EventList.Add(newEvent);
            
        }

        public void AddPage()
        {
            EventOnOnePage eventOnOne=new EventOnOnePage();
            //eventOnOne.thisPage = GameManager.Instance.currentPage;
            allEvents.Add(eventOnOne);
        }

        public void DeletePage()
        {
            allEvents.RemoveAt(GameManager.Instance.pageIdx);
        }
    }
}


