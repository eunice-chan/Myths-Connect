using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct Myth
{
    // Latitude
    public float MaxRangeX { get; set; }
    public float MinRangeX { get; set; }

    // Longitude
    public float MaxRangeY { get; set; }
    public float MinRangeY { get; set; }

    // Myth Name
    public string name { get; set; }
    // Myth Content
    public string content { get; set; }
    // Myth Source (what group did it come from?)
    public string source { get; set; }

    public Myth(string content) : this(new float[] {0, 0}, new float[] {0, 0}, "Everywhere", "Default", content) {}

    public Myth(float[] X, float[] Y, string MythSource, string MythName, string MythContent)
    {
        MaxRangeX = Math.Max(X[0], X[1]);
        MinRangeX = Math.Min(X[0], X[1]);
        MaxRangeY = Math.Max(Y[0], Y[1]);
        MinRangeY = Math.Min(Y[0], Y[1]);
        name = MythName;
        content = MythContent;
        source = MythSource;
    }
}


public class MythOnTap : MonoBehaviour
{
    // TODO: Replace this with a server-side database later so it can scale up nicely.
    Myth[] ListOfAllMyths =
        {
        new Myth(
            new float[] {40, 42}, new float[] {-123, -119},
            "Achomawi", "Creation and Longevity",
            "Coyote began the creation of the earth, but Eagle completed it. Coyote " +
            "scratched it up with his paws out of nothingness, but Eagle complained " +
            "there were no mountains for him to perch on. So Coyote made hills, but " +
            "they were not high enough. Therefore Eagle scratched up great ridges. " +
            "When Eagle flew over them, his feathers dropped down, took root, and " +
            "became trees. The pin feathers became bushes and plants. " +
            "Coyote and Fox together created man. They quarrelled as to whether they " +
            "should let men live always or not. Coyote said, \"If they want to die, " +
            "let them die.\" Fox said, \"If they want to come back, let them come " +
            "back.\" But Coyote's medicine was stronger, and nobody ever came back. " +
            "Coyote also brought fire into the world, for the Indians were freezing. " +
            "He journeyed far to the west, to a place where there was fire, stole " +
            "some of it, and brought it home in his ears. He kindled a fire in the " +
            "mountains, and the Indians saw the smoke of it, and went up and got " +
            "fire."),
        new Myth(
            new float[] {41, 44}, new float[] {-123, -120},
            "Shastika", "Old Mole's Creation",
            "Long, long ago, before there was any earth, Old Mole burrowed underneath " +
            "Somewhere, and threw up the earth which forms the world. Then Great Man " +
            "created the people. But the Indians were cold. Now in the cast gleamed " +
            "the white Fire Stone. Therefore Coyote journeyed eastward, and brought " +
            "back the Fire Stone for the Indians. So people had fire. In the " +
            "beginning, Sun had nine brothers, all flaming hot like himself. But Coyote " +
            "killed the nine brothers and so saved the world from burning up. But Moon " +
            "also had nine brothers all made of ice, like himself, and the Night People " +
            "almost froze to death. Therefore Coyote went away out on the eastern edge " +
            "of the world with his flint-stone knife. He heated stones to keep his hands " +
            "warm, and as the Moons arose, he killed one after another with his " +
            "flint-stone knife, until he had slain nine of them. Thus the people were " +
            "saved from freezing at night. When it rains, some Indian, sick in heaven, " +
            "is weeping. Long, long ago, there was a good young Indian on earth. When he " +
            "died the Indians wept so that a flood came upon the earth, and drowned all " +
            "people except one couple."),
        new Myth(
            new float[] {36, 38}, new float[] {-118, -117},
            "Pai Ute", "Pokoh, the Old Man",
            "Pokoh, Old Man, they say, created the world. Pokoh had many thoughts. He had " +
            "many blankets in which he carried around gifts for men. He created every " +
            "tribe out of the soil where they used to live. That is why an Indian wants to " +
            "live and die in his native place. He was made of the same soil. Pokoh did not " +
            "wish men to wander and travel, but to remain in their birthplace. Long ago, " +
            "Sun was a man, and was bad. Moon was good. Sun had a quiver full of arrows, " +
            "and they are deadly. Sun wishes to kill all things. Sun has two daughters " +
            "(Venus and Mercury) and twenty men kill them; but after fifty days, they return " +
            "to life again. Rainbow is the sister of Pokoh, and her breast is covered with " +
            "flowers. Lightning strikes the ground and fills the flint with fire. That is the " +
            "origin of fire. Some say the beaver brought fire from the east, hauling it on his " +
            "broad, flat tail. That is why the beaver's tail has no hair on it, even to this " +
            "day. It was burned off. There are many worlds. Some have passed and some are still " +
            "to come. In one world the Indians all creep; in another they all walk; in another " +
            "they all fly. Perhaps in a world to come, Indians may walk on four legs; or they " +
            "may crawl like snakes; or they may swim in the water like fish."),
        new Myth(
            new float[] {39, 41}, new float[] {-122, -120},
            "Maidu", "Thunder and Lightning",
            "Great-Man created the world and all the people. At first the earth was very hot, so " +
            "hot it awas melted, and that is why even to-day there is fire in the trunk and " +
            "branches of trees, and in the stones. Lightning is Great-Man himself coming down " +
            "swiftly from his world above, and tearing apart the trees with his flaming arm. " +
            "Thunder and Lightning are two great spirits who try to destroy mankind. But Rainbow " +
            "is a good spirit who speaks gently to them, and persuades them to let the Indians " +
            "live a little longer."),
        new Myth(
            new float[] {38, 39}, new float[] {-123, -121},
            "Ashochimi", "Legend of the Flood",
            "Long ago there was a great flood which destroyed all the people in the world. Only " +
            "Coyote was saved. When the waters subsided, the earth was empty. Coyote thought about " +
            "it a long time. Then Coyote collected a great bundle of tail feathers from owls, hawks, " +
            "eagles, and buzzards. He journeyed over the whole earth and carefully located the site " +
            "of each Indian village. Where the tepees had stood, he planted a feather in the ground " +
            "and scraped up the dirt around it. The feathers sprouted like trees, and grew up and " +
            "branched. At last they turned into men and women. So the world was inhabited with people " +
            "again."),
        new Myth(
            new float[] {35, 38}, new float[] {-121, -119},
            "Yokuts", "Origin of the Sierra Nevadas and Coast Range",
            "Once there was a time when there was nothing in the world but water. About the place where " +
            "Tulare Lake is now, there was a pole standing far up out of the water, and on this pole " +
            "perched Hawk and Crow. First Hawk would sit on the pole a while, then Crow would knock him off " +
            "and sit on it himself. Thus they sat on the top of the pole above the water for many ages. At " +
            "last they created the birds which prey on fish. They created Kingfisher, Eagle, Pelican, and " +
            "others. They created also Duck. Duck was very small but she dived to the bottom of the water, took " +
            "a beakful of mud, and then died in coming to the top of the water. Duck lay dead floating on the " +
            "water. Then Hawk and Crow took the mud from Duck's beak, and began making the mountains. They began " +
            "at the place now known as Ta-hi-cha-pa Pass, and Hawk made the east range. Crow made the west one. " +
            "They pushed the mud down hard into the water and then piled it high. They worked toward the north. " +
            "At last Hawk and Crow met at Mount Shasta. Then their work was done. But when they looked at their " +
            "mountains, Crow's range was much larger than Hawk's. Hawk said to Crow, \"How did this happen, you " +
            "rascal? You have been stealing earth from my bill. That is why your mountains are the biggest.\" Crow " +
            "laughed. Then Hawk chewed some Indian tobacco. That made him wise. At once he took hold of the mountains " +
            "and turned them around almost in a circle. He put his range where Crow's had been. That is why the Sierra " +
            "Nevada Range is larger than the Coast Range."),
        new Myth(
            new float[] {37, 38}, new float[] {-120, -119},
            "Yosemite Valley", "Legend of Tis-se'-yak (South Dome and North Dome)",
            "Tisseyak and her husband journeyed from a country very far off, and entered the valley of the Yosemite " +
            "foot-sore from travel. She bore a great heavy conical basket, strapped across her head. Tisseyak came first. " +
            "Her husband followed with a rude staff and a light roll of skins on his back. They were thirsty after their " +
            "long journey across the mountains. They hurried forward to drink of the waters, and the woman was still in " +
            "advance when she reached Lake Awaia. Then she dipped up the water in her basket and drank of it. She drank " +
            "up all the water. The lake was dry before her husband reached it. And because the woman drank all the water, " +
            "there came a drought. The earth dried tip. There was no grass, nor any green thing. But the man was angry " +
            "because he had no water to drink. He beat the woman with his staff and she fled, but he followed and beat her " +
            "even more. Then the woman wept. In her anger she turned and flung her basket at the man. And even then they " +
            "were changed into stone. The woman's basket lies upturned beside the man. The woman's face is tear-stained, " +
            "with long dark lines trailing down. South Dome is the woman and North Dome is the husband. The Indian woman " +
            "cuts her hair straight across the forehead, and allows the sides to drop along her cheeks, forming a square face."),
        new Myth(
            new float[] {34, 38}, new float[] {-114, -106},
            "Navajo", "The Boy who Became a God",
            "The Tolchini, a clan of the Navajos, lived at Wind Mountains. One of them used to take long visits into the " +
            "country. His brothers thought he was crazy. The first time on his return, he brought with him a pine bough; " +
            "the second time, corn. Each time he returned he brought something new and had a strange story to tell. His " +
            "brothers said: \"He is crazy. He does not know what he is talking about.\" Now the Tolchini left Wind Mountains " +
            "and went to a rocky foothill east of the San Mateo Mountain. They had nothing to eat but seed grass. The eldest " +
            "brother said, \"Let us go hunting,\" but they told the youngest brother not to leave camp. But five days and five " +
            "nights passed, and there was no word. So he followed them. After a day's travel he camped near a canon, in a " +
            "cavelike place. There was much snow but no water so he made a fire and heated a rock, and made a hole in the " +
            "ground. The hot rock heated the snow and gave him water to drink. just then he heard a tumult over his head, like " +
            "people passing. He went out to see what made the noise and saw many crows crossing back and forth over the canon. " +
            "This was the home of the crow, but there were other feathered people there, and the chaparral cock. He saw many " +
            "fires made by the crows on each side of the ca–on. Two crows flew down near him and the youth listened to hear what " +
            "was the matter. The two crows cried out, \"Somebody says. Somebody says.\" The youth did not know what to make of " +
            "this. A crow on the opposite side called out, \"What is the matter? Tell us! Tell us! What is wrong?\" The first " +
            "two cried out, \"Two of us got killed. We met two of our men who told us.\" Then they told the crows how two men " +
            "who were out hunting killed twelve deer, and a party of the Crow People went to the deer after they were shot. " +
            "They said, \"Two of us who went after the blood of the deer were shot.\" The crows on the other side of the ca–on " +
            "called, \"Which men got killed?\" \"The chaparral cock, who sat on the horn of the deer, and the crow who sat on " +
            "its backbone.\" The others called out, \"We are not surprised they were killed. That is what we tell you all the " +
            "time. If you go after dead deer you must expect to be killed.\" \"We will not think of them longer,\" so the two " +
            "crows replied. \"They are dead and gone. We are talking of things of long ago.\" But the youth sat quietly below " +
            "and listened to everything that was said. After a while the crows on the other side of the canon made a great " +
            "noise and began to dance. They had many songs at that time. The youth listened all the time. After the dance a great " +
            "fire was made and he could see black objects moving, but he could not distinguish any people. He recognized the voice " +
            "of Hasjelti. He remembered everything in his heart. He even remembered the words of the songs that continued all " +
            "night. He remembered every word of every song. He said to himself, \"I will listen until daylight.\" The Crow People " +
            "did not remain on the side of the canon where the fires were first built. They crossed and recrossed the canon in their " +
            "dance. They danced back and forth until daylight. Then all the crows and the other birds flew away to the west. All that " +
            "was left was the fires and the smoke. Then the youth started for his brothers' camp. They saw him coming. They said, \"He " +
            "will have lots of stories to tell. He will say he saw something no one ever saw.\" But the brother-in-law who was with " +
            "them said, \"Let him alone. When he comes into camp he will tell us all. I believe these things do happen for he could not " +
            "make up these things all the time.\" Now the camp was surrounded by pinon brush and a large fire was burning in the centre. " +
            "There was much meat roasting over the fire. When the youth reached the camp, he raked over the coals and said. \"I feel " +
            "cold.\" Brother-in-law replied, \"It is cold. When people camp together, they tell stories to one another in the morning. " +
            "We have told ours, now you tell yours.\" The youth said, \"Where I stopped last night was the worst camp I ever had.\" The " +
            "brothers paid no attention but the brother-in-law listened. The youth said, \"I never heard such a noise.\" Then he told his " +
            "story. Brother-in-law asked what kind of people made the noise. The youth said, \"I do not know. They were strange people to me, " +
            "but they danced all night back and forth across the canon and I heard them say my brothers killed twelve deer and afterwards killed " +
            "two of their people who went for the blood of the deer. I heard them say, \"'That is what must be expected. If you go to such places, " +
            "you must expect to be killed.' \" The elder brother began thinking. He said, \"How many deer did you say were killed?\" \"Twelve.\" " +
            "Elder brother said, \"I never believed you before, but this story I do believe. How do you find out all these things? What is the " +
            "matter with you that you know them?\" The boy said, \"I do not know. They come into my mind and to my eyes.\" Then they started " +
            "homeward, carrying the meat. The youth helped them. As they were descending a mesa, they sat down on the edge to rest. Far down " +
            "the mesa were four mountain sheep. The brothers told the youth to kill one. The youth hid in the sage brush and when the sheep " +
            "came directly toward him, he aimed his arrow at them. But his arm stiffened and became dead. The sheep passed by. He headed them " +
            "off again by hiding in the stalks of a large yucca. The sheep passed within five steps of him, but again his arm stiffened as he " +
            "drew the bow. He followed the sheep and got ahead of them and hid behind a birch tree in bloom. He had his bow ready, but as they " +
            "neared him they became gods. The first was Hasjelti, the second was Hostjoghon, the third Naaskiddi, and the fourth Hadatchishi. " +
            "Then the youth fell senseless to the ground. The four gods stood one on each side of him, each with a rattle. They traced with " +
            "their rattles in the sand the figure of a man, drawing lines at his head and feet. Then the youth recovered and the gods again " +
            "became sheep. They said, \"Why did you try to shoot us? You see you are one of us.\" For the youth had become a sheep. The gods " +
            "said, \"There is to be a dance, far off to the north beyond the Ute Mountain. We want you to go with us. We will dress you like " +
            "ourselves and teach you to dance. Then we will wander over the world.\" Now the brothers watched from the top of the mesa but they " +
            "could not see what the trouble was. They saw the youth lying on the ground, but when they reached the place, all the sheep were gone. " +
            "They began crying, saying, \"For a long time we would not believe him, and now he has gone off with the sheep.\" They tried to head off " +
            "the sheep, but failed. They said, \"If we had believed him, he would not have gone off with the sheep. But perhaps some day we will see " +
            "him again.\" At the dance, the five sheep found seven others. This made their number twelve. They journeyed all around the world. All " +
            "people let them see their dances and learn their songs. Then the eleven talked together and said, \"There is no use keeping this youth " +
            "with us longer. He has learned everything. He may as well go back to his people and teach them to do as we do.\" So the youth was taught " +
            "to have twelve in the dance, six gods and six goddesses, with Hasjelti to lead them. He was told to have his people make masks to represent " +
            "the gods. So the youth returned to his brothers, carrying with him all songs, all medicines, and clothing."),
        new Myth(
            new float[] {35, 36}, new float[] {-108, -106},
            "Sia", "Coyote and the Fawns",
            "Another day when he was travelling around, Coyote met a deer with two fawns. The fawns were beautifully spotted, and he said to the deer, " +
            "\"How did you paint your children? They are so beautiful!\" Deer replied, \"I painted them with fire from the cedar.\" \"And how did you do " +
            "the work?\" asked Coyote. \"I put my children into a cave and built a fire of cedar in front of it. Every time a spark flew from the fire it " +
            "struck my children, making a beautiful spot.\" \"Oh,\" said Coyote, \"I will do the same thing. Then I will make my children beautiful.\" He " +
            "hurried to his house and put his children in a cave. Then he built a fire of cedar in front of it and stood off to watch the fire. But the " +
            "children cried because the fire was very hot. Coyote kept calling to them not to cry because they would be beautiful like the deer. After a " +
            "time the crying ceased and Coyote was pleased. But when the fire died down, he found they were burned to death. Coyote expected to find them " +
            "beautiful, but instead they were dead. Then he was enraged with the deer and ran away to hunt her, but he could not find her anywhere. He was " +
            "much distressed to think the deer had fooled him so easily."),
        new Myth(
            new float[] {36, 38}, new float[] {-118, -117},
            "Pai Ute", "Song of the Ghost Dance",
            "The snow lies there - ro-rani! The snow lies there - ro-rani! The snow lies there - ro-rani! The snow lies there - ro-rani! The Milky Way lies " +
            "there. The Milky Way lies there. \"This is one of the favorite songs of the Paiute Ghost dance. . . . It must be remembered that the dance is " +
            "held in the open air at night, with the stars shining down on the wide-extending plain walled in by the giant Sierras, fringed at the base with " +
            "dark pines, and with their peaks white with eternal snows. Under such circumstances this song of the snow lying white upon the mountains, and " +
            "the Milky Way stretching across the clear sky, brings up to the Paiute the same patriotic home love that comes from lyrics of singing birds and " +
            "leafy trees and still waters to the people of more favored regions. . . . The Milky Way is the road of the dead to the spirit world.\""),

		new Myth(
			new float[] {20, 50}, new float[] {-50, -200},
			"CALIFORNA", "TEST MYTH NAMEEEEEE",
			"The snow lies there - ro-rani! The snow lies there - ro-rani! The snow lies there - ro-rani! The snow lies there - ro-rani!"+
			"Another day when he was travelling around, Coyote met a deer with two fawns. The fawns were beautifully spotted, and he said to the deer, " +
			"\"How did you paint your children? They are so beautiful!\" Deer replied, \"I painted them with fire from the cedar.\" \"And how did you do " +
			"the work?\" asked Coyote. \"I put my children into a cave and built a fire of cedar in front of it. Every time a spark flew from the fire it " +
			"struck my children, making a beautiful spot.\" \"Oh,\" said Coyote, \"I will do the same thing. Then I will make my children beautiful.\" He " +
			"hurried to his house and put his children in a cave. Then he built a fire of cedar in front of it and stood off to watch the fire. But the " +
			"children cried because the fire was very hot. Coyote kept calling to them not to cry because they would be beautiful like the deer. After a " +
			"time the crying ceased and Coyote was pleased. But when the fire died down, he found they were burned to death. Coyote expected to find them " +
			"beautiful, but instead they were dead. Then he was enraged with the deer and ran away to hunt her, but he could not find her anywhere. He was " +
			"much distressed to think the deer had fooled him so easily.")
	};

    GameObject player;
    static System.Random rand = new System.Random();

	public CanvasGroup canvasGroup;
	public Text title;
	public Text content;

    void Start()
    {
        player = GameObject.Find("Player");
		canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
		title = GameObject.Find("Title").GetComponent<Text>();
		content = GameObject.Find("Text").GetComponent<Text>();
		HideUI();

	}

	void HideUI()
	{
        // Make everything transparent
		canvasGroup.alpha = 0f;
        // Prevent UI elements from receiving input
		canvasGroup.blocksRaycasts = false;
	}

    void ShowUI()
	{
		// Make everything opaque
		canvasGroup.alpha = 1f;
		// Allow UI elements to receive input
		canvasGroup.blocksRaycasts = true;
	}

	bool InRange(float val, float min, float max)
    {
        return min <= val && val <= max;
    }

    List<Myth> ListIfMythsNearCoord(float x, float y)
    {
        List<Myth> MythsNearCoord = new List<Myth>();
        foreach (Myth myth in ListOfAllMyths)
        {
            if (InRange(x, myth.MinRangeX, myth.MaxRangeX) && InRange(y, myth.MinRangeY, myth.MaxRangeY))
            {
                MythsNearCoord.Add(myth);
            }
        }
        return MythsNearCoord;
    }

    Myth GenerateMyth()
    {
        Vector3 playerPosition = player.transform.position;
        float x = playerPosition.x;
        float y = playerPosition.z;
        List<Myth> MythsNearCoord = ListIfMythsNearCoord(x, y);
        print(MythsNearCoord.Count);
        if (MythsNearCoord.Count > 0)
        {
            return MythsNearCoord[rand.Next(MythsNearCoord.Count)];

        }
        else
        {
            return new Myth("Hello World!");
        }
    }
	void OnMouseUpAsButton()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			Myth myth = GenerateMyth();
			title.text = myth.source + ": " + myth.name;
			content.text = myth.content;
			ShowUI();
		}
	}
}
