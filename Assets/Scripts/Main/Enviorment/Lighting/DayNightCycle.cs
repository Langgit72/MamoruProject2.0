using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


using TMPro;

public class DayNightCycle : MonoBehaviour
{
    float multiplier = 6.2831f; //2pi
    public float dropOff = 1; //ratio of day time to runrise time 0-1
    public float dayLength = 1; //length of a whole day-night cycle in in seconds
    public float mod1 = 70f;
    public float mod2 = 20f;
    public float startTime;


    public string period="Night"; //Clock strings
    string meridiem = "AM";

    public Light2D globalLight; //day light

    Phases moonPhases;
    
    public Transform sun; 
    public Transform moon;

    public SpriteRenderer day;
    public SpriteRenderer night;

    public Color og_day;
    public Color og_night;

    public TMP_Text clockText;
    public TMP_Text periodText;

    // Start is called before the first frame update
    public static DayNightCycle instance; // the single instance of inventory

    public Transform center;


    #region Unity Methods
    void Awake()
    {
        #region Singleton instance
        if (instance == null)
        {
            instance = this; //if there is no current inventory instance in game, occupy singleton with current inventory class
        }
        else
        {
            Debug.LogWarning("More than one instance"); // otherwise there is already an inventory instance in game, and no more can be created
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }

    void Start()
    {
        if (day!=null) {
            og_day = day.color;
            og_night = night.color;
        }
        og_day.a = 0;
        og_night.a = 255;
        moonPhases = moon.GetComponent<Phases>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Day Light Intensity and Sky Apperance
        /*
        if (period == "Dusk" || period == "Night")
        {
            globalLight.intensity = moonPhases.phaseLights[moonPhases.phaseNumber];
        }
        else
        {
            globalLight.intensity = og_day.a;
        }
        */
        globalLight.intensity = og_day.a;
        float m_Time = Time.time + startTime;
        float sunRise = calculateSunrise(m_Time);


        if (day!=null) {
            day.color = og_day;
            night.color = og_night;


            og_day.a = 1 - sunRise; //blue increases with the sunrise
            og_night.a = sunRise;
            og_day.b = 1 - sunRise * .75f; //blues and greens decreases with the sunrise.sunset
            og_day.g = 1 - sunRise * .75f;
            og_day.r = sunRise * .85f; //reds increase with the sunrise.sunset
        }

        #endregion

        float specialTime = m_Time + dayLength / 24;
        #region Sun and Moon Animation
        if (moon != null&&sun!=null)
        {
            moon.position = new Vector3(center.position.x + Mathf.Sin((specialTime) * multiplier / dayLength) * mod1, center.position.y + Mathf.Cos((specialTime) * multiplier / dayLength) * mod2, moon.position.z);
            sun.position = new Vector3(center.position.x + Mathf.Sin((specialTime + dayLength / 2) * multiplier / dayLength) * mod1, center.position.y + Mathf.Cos((specialTime + dayLength / 2) * multiplier / dayLength) * mod2, sun.position.z);
        }
       



      
        #endregion

        #region Clock
        meridiem = (((m_Time+dayLength/24) / dayLength) % 1) <= 0.5 ? "AM" : "PM";
        float hour = Mathf.Floor(((m_Time * 12 / dayLength) * 2 % 12) + 1);
        string timeStr =  hour + ":00 " + meridiem;

        if (timeStr=="6:00 AM")
        {
            period = "Dawn";
            if (AudioController.instance.jobTable.Contains(AudioType.ST_01)){
                AudioController.instance.StopAudio(AudioType.ST_01);
                AudioController.instance.StopAudio(AudioType.STA_01);
            }
            if (AudioController.instance.jobTable.Contains(AudioType.STA_01))
            {
                AudioController.instance.StopAudio(AudioType.STA_01);
            }
            if (!AudioController.instance.jobTable.Contains(AudioType.AMB_01))
            {
                AudioController.instance.LoopAudio(AudioType.AMB_01);
            }
        }
        else if (timeStr == "9:00 AM")
        {
            if(period == "Dawn")
            {
                updatePhase();
            }

            period = "Day";
            if (!AudioController.instance.jobTable.Contains(AudioType.ST_03))
            {AudioController.instance.LoopAudio(AudioType.ST_03);
            }
        }
        else if (timeStr == "6:00 PM")
        {
            period = "Dusk";
            if (AudioController.instance.jobTable.Contains(AudioType.ST_03))
            { AudioController.instance.StopAudio(AudioType.ST_03);
            }
            if (!AudioController.instance.jobTable.Contains(AudioType.AMB_02))
            {
                AudioController.instance.LoopAudio(AudioType.AMB_02);
            }
        }
        else if (timeStr == "9:00 PM")
        {
            period = "Night";
            if (!AudioController.instance.jobTable.Contains(AudioType.ST_01))
            {AudioController.instance.LoopAudio(AudioType.ST_01);
             //AudioController.instance.LoopAudio(AudioType.STA_01);
            }
        }
        periodText.text = period;
        clockText.text = timeStr;

        #endregion
    }

    #endregion

    #region Methods
    float calculateSunrise(float time)
    {
        time += 0;
        float intensity = 0.5f;
        float output = ((Mathf.Sqrt((1 + Mathf.Pow(dropOff, 2) / (1 + (Mathf.Pow(dropOff, 2) * Mathf.Pow(Mathf.Cos(time * 1 / dayLength * multiplier), 2))))) * Mathf.Cos(time * 1 / dayLength * multiplier)) * intensity) + intensity;
        return output;
    }
    void updatePhase()
    {
        if (moonPhases.phaseNumber + 1 == moonPhases.phases.Count)
        {
            moonPhases.phaseNumber = 0;
        }
        else
        {
            moonPhases.phaseNumber += 1;
        }
    }
    #endregion
}




