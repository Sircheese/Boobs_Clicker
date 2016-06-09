using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    #region variables
    const int maxShopSize = 9;
    const int maxUpgradesSize = 9;
    const int maxTitsLevel = 5;
    const float ppsticks = 0.05f;

    float points;
    Text pointsText;
    int pps = 0;
    Text ppsText;

    int titsLevel;

    float timer;

    Image titsButton;
    Image saveButton;
    Image loadButton;
    Image upgradesButton;
    Image closeUpgradesButton;

    public Sprite[] titsSprites = new Sprite [maxTitsLevel];

    GameObject upgradesWindow;
    GameObject shopContent;
    public GameObject gainedpoints;

    #region shop
        GameObject shopWindow;
        Image shopButton;
        Image closeShopButton;
        struct CacheShop
        {
            public Image img;
            public Text name;
            public Text info;
            public Text insetLVLText;
            public Text costText;
        }
        CacheShop[] shopInsets;
        struct ShopStruct
        {
            public string name;
            public string info;
            public Sprite sprite;
            public int insetLVL;
            public float cost;
            public float pointsperLVL;
            public float upgradeValue;
            public bool canBuy;
            public int[] uplvls;
        }
        ShopStruct[] shopData;
        public Sprite[] shopImages = new Sprite[maxShopSize];
    #endregion


    bool showWindow = true;
    #endregion


    public delegate void MyDelegate();

    // Use this for initialization
    void Start()
    {
        shopData = new ShopStruct[maxShopSize];
        shopInsets = new CacheShop[maxShopSize];
        cahceData();
        initData();
        refreshPoints();
        refreshPPS();

        OnClickDownListener(titsButton.gameObject, () => { clickTit(); });

        OnClickDownListener(shopButton.gameObject, () => { controllWindow(shopWindow); });
        OnClickDownListener(closeShopButton.gameObject, () => { controllWindow(shopWindow); });

        OnClickDownListener(upgradesButton.gameObject, () => { controllWindow(upgradesWindow); });
        OnClickDownListener(closeUpgradesButton.gameObject, () => { controllWindow(upgradesWindow); });
    }



    #region initialization
    void cahceData()
    {
        pointsText = GameObject.Find("PointsText").GetComponent<Text>();
        ppsText = GameObject.Find("PPSText").GetComponent<Text>();

        titsButton = GameObject.Find("TitsImage").GetComponent<Image>();

        shopButton = GameObject.Find("ShopButton").GetComponent<Image>();
        shopWindow = GameObject.Find("ShopCanvas");
        closeShopButton = GameObject.Find("CloseShopButton").GetComponent<Image>();
        shopContent = GameObject.Find("ShopContent");

        upgradesButton = GameObject.Find("UpgradesButton").GetComponent<Image>();
        upgradesWindow = GameObject.Find("UpgradesCanvas");
        closeUpgradesButton = GameObject.Find("CloseUpgradesButton").GetComponent<Image>();

        loadButton = GameObject.Find("LoadButton").GetComponent<Image>();
        saveButton = GameObject.Find("SaveButton").GetComponent<Image>();

        int temp_ = 0;
        foreach(Transform t in shopContent.transform)
        {
            int cachetemp_ = temp_;
            {
                shopInsets[temp_].info = t.transform.Find("Info").GetComponent<Text>();
                shopInsets[temp_].name = t.transform.Find("Name").GetComponent<Text>();
                shopInsets[temp_].img = t.transform.Find("Image").GetComponent<Image>();
                shopInsets[temp_].insetLVLText = t.transform.Find("LVL").GetComponent<Text>();
                shopInsets[temp_].costText = t.transform.Find("Cost").GetComponent<Text>();
                OnClickDownListener(t.transform.Find("BuyButton").gameObject, () =>
                {
                    buyShop(cachetemp_);
                });
                if (temp_ > 0)
                {
                    shopInsets[temp_].name.color = Color.gray;
                    shopInsets[temp_].info.color = Color.gray;
                    shopInsets[temp_].img.color = Color.gray;
                    shopInsets[temp_].insetLVLText.color = Color.gray;
                    shopInsets[temp_].costText.color = Color.gray;
                    t.transform.Find("BuyButton").GetComponent<Image>().color = Color.gray;
                };
                temp_++;
            }
        };
    }

    void initData()
    {
        points = 0;
        pps = 1;

        titsLevel = 0;

        shopWindow.SetActive(false);
        upgradesWindow.SetActive(false);
        shopInit();
        pushshopData();
    }

    void shopInit()
    {

        for (int i = 0; i < maxShopSize; i++)
        {
            shopData[i].upgradeValue = 0;
            shopData[i].uplvls = new int[3];
        }
        int ID = 0;
        shopData[ID].name = "Брошь";
        shopData[ID].info = "вап";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 100;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 1;
        shopData[ID].uplvls[0] = 0;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Духи";
        shopData[ID].info = "олб";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 345;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 2;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Кулон";
        shopData[ID].info = "нгл";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 567;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 3;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Пирсинг";
        shopData[ID].info = "рптмти";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 5758;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 1;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Пудренница";
        shopData[ID].info = "апра";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].pointsperLVL = 1;
        shopData[ID].cost = 100;
        shopData[ID].insetLVL = 0;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Тушь";
        shopData[ID].info = "оь";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 100;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 1;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Браслет";
        shopData[ID].info = "кер";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 100;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 1;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Помада";
        shopData[ID].info = "кно";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 100;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 1;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
        ID++;
        shopData[ID].name = "Серьги";
        shopData[ID].info = "роь";
        shopData[ID].sprite = shopImages[ID];
        shopData[ID].cost = 100;
        shopData[ID].insetLVL = 0;
        shopData[ID].pointsperLVL = 1;
        shopData[ID].uplvls[0] = 1;
        shopData[ID].uplvls[0] = 3;
        shopData[ID].uplvls[0] = 5;
    }
    #endregion



    #region output
    void refreshPoints()
    {
        pointsText.text = "Points: " + points;
    }

    void refreshPPS()
    {
        ppsText.text = "PPS: " + pps;
    }

    void pushshopData()
    {
        for (int i = 0; i < maxShopSize; i++)
        {
            shopInsets[i].name.text = shopData[i].name;
            shopInsets[i].info.text = shopData[i].info;
            shopInsets[i].img.sprite = shopData[i].sprite;
            shopInsets[i].insetLVLText.text = shopData[i].insetLVL + "";
            shopInsets[i].costText.text = shopData[i].cost + "";
        };
    }
    #endregion


    #region game
    void clickTit()
    {
        var tmpobject = Instantiate(gainedpoints);
        AddPointsScript _tmp = tmpobject.GetComponent<AddPointsScript>();
        _tmp.x = Input.mousePosition.x;
        _tmp.y = Input.mousePosition.y - (Input.mousePosition.y - Screen.height / 2) * 2;
        points += 10;
        refreshPoints();
    }

    void buyShop(int _id)
    {

    }

    void controllWindow(GameObject _window)
    {
        _window.SetActive(showWindow);
        showWindow = !showWindow;
        saveButton.gameObject.SetActive(showWindow);
        loadButton.gameObject.SetActive(showWindow);
        shopButton.gameObject.SetActive(showWindow);
        upgradesButton.gameObject.SetActive(showWindow);
    }
    #endregion



    #region save/load
    void save()
    {
        //todo
    }

    void load()
    {
        //todo 
    }

    void saveInt(string _varName, int _varValue)
    {
        PlayerPrefs.SetInt(_varName, _varValue);
    }

    void saveFloat(string _varName, int _varValue)
    {
        PlayerPrefs.SetFloat(_varName, _varValue);
    }

    int loadInt(string _varName)
    {

        return PlayerPrefs.GetInt(_varName); 
    }

    float loadFloat(string _varName)
    {

        return PlayerPrefs.GetFloat(_varName);
    }
    #endregion

    void OnClickDownListener(GameObject obj, MyDelegate ThisMethod) //МЕТОД КЛИКА МЫШЬЮ
    {
        if (obj.GetComponent<OnClickDownController>() == null)
        {
            obj.AddComponent<OnClickDownController>().Method = () => { ThisMethod(); };
        }
        else
        {
            obj.GetComponent<OnClickDownController>().Method = () => { ThisMethod(); };
        }
    }


    // Update is called once per frame
    void Update ()
    {
        if (timer < ppsticks)
        {
            timer += Time.deltaTime;
        }
        else
        {
            points += pps * ppsticks;
            refreshPoints();
            timer = 0;
        };
    }
}
