using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is the Item DB and Receipes (Not Yet Inplemented)
 * @Author Omar Radwan
 * @Version 0.5.0
 * @Throws System.Exception
 */
public static class ItemDB
{
    private static bool initDB = false;
    private static Dictionary<string, Item> itemDB = new Dictionary<string, Item>();

    // Add items to the Dictionary
    // @Parms PlayerManager playerMan : The PlayerManager to use
    public static void initDatabase(PlayerManager playerMan) {
        if (!initDB) {
            Eatable water = new Eatable("Water Bottle", "A plasic water bottle full of water... I think.", playerMan, 5.0f, 25.0f, 0.0f);
            addItem("waterBottle", water);
            Eatable cerealBar = new Eatable("Cereal Bar", "A cereal bar still in the wrapper. Feels like it broken into pieces thought.", playerMan, 10.0f, -2.0f, 0.0f);
            addItem("cerealBar", cerealBar);
            Eatable pills = new Eatable("Pills", "A bunch of random pills. Not sure what type but what do you have to lose?", playerMan, 0.0f, 0.0f, 25.0f);
            addItem("pills", pills);
            
            Lore note1 = new Lore("Note - Test", "This is a test note", "According to all known laws of aviation, there is no way a bee should be able to fly."
            + "Its wings are too small to get its fat little body off the ground. The bee, of course, flies anyway because bees don't care what humans think is"
            + "impossible. Yellow, black. Yellow, black. Yellow, black. Yellow, black. Ooh, black and yellow! Let's shake it up a little. Barry! Breakfast is"
            + "ready! Ooming! Hang on a second. Hello? - Barry? - Adam? - Oan you believe this is happening? - I can't. I'll pick you up. Looking sharp. Use the"
            + "stairs. Your father paid good money for those. Sorry. I'm excited. Here's the graduate. We're very proud of you, son. A perfect report card, all B's.");
            addItem("testNote", note1);
            Lore note2 = new Lore("Long Note", "This is a test note that is long", "According to all known laws of aviation, there is no way a bee should be able to fly."
            + "Its wings are too small to get its fat little body off the ground. The bee, of course, flies anyway because bees don't care what humans think is impossible."
            + "Yellow, black. Yellow, black. Yellow, black. Yellow, black. Ooh, black and yellow! Let's shake it up a little. Barry! Breakfast is ready! Ooming! Hang on a"
            + "second. Hello? - Barry? - Adam? - Oan you believe this is happening? - I can't. I'll pick you up. Looking sharp. Use the stairs. Your father paid good money"
            + "for those. Sorry. I'm excited. Here's the graduate. We're very proud of you, son. A perfect report card, all B's. Very proud. Ma! I got a thing going here."
            + "- You got lint on your fuzz. - Ow! That's me! - Wave to us! We'll be in row 118,000. - Bye! Barry, I told you, stop flying in the house! - Hey, Adam. - Hey,"
            + "Barry. - Is that fuzz gel? - A little. Special day, graduation. Never thought I'd make it. Three days grade school, three days high school. Those were awkward."
            + "Three days college. I'm glad I took a day and hitchhiked around the hive. You did come back different. - Hi, Barry. - Artie, growing a mustache? Looks good."
            + "- Hear about Frankie? - Yeah. - You going to the funeral? - No, I'm not going. Everybody knows, sting someone, you die. Don't waste it on a squirrel. Such a"
            + "hothead. I guess he could have just gotten out of the way. I love this incorporating an amusement park into our day. That's why we don't need vacations. Boy,"
            + "quite a bit of pomp... under the circumstances. - Well, Adam, today we are men. - We are! - Bee-men. - Amen! Hallelujah! Students, faculty, distinguished bees,"
            + "please welcome Dean Buzzwell. Welcome, New Hive Oity graduating class of... ...9:15. That concludes our ceremonies. And begins your career at Honex Industries!"
            + "Will we pick ourjob today? I heard it's just orientation. Heads up! Here we go. Keep your hands and antennas inside the tram at all times. - Wonder what it'll be like?"
            + "- A little scary. Welcome to Honex, a division of Honesco and a part of the Hexagon Group. This is it! Wow. Wow. We know that you, as a bee, have worked your whole life"
            + "to get to the point where you can work for your whole life. Honey begins when our valiant Pollen Jocks bring the nectar to the hive. Our top-secret formula is automatically"
            + "color-corrected, scent-adjusted and bubble-contoured into this soothing sweet syrup with its distinctive golden glow you know as... Honey! - That girl was hot. - She's my"
            + "cousin! - She is? - Yes, we're all cousins. - Right. You're right. - At Honex, we constantly strive to improve every aspect of bee existence. These bees are stress-testing"
            + "a new helmet technology. - What do you think he makes? - Not enough. Here we have our latest advancement, the Krelman. - What does that do? - Oatches that little strand of"
            + "honey that hangs after you pour it. Saves us millions. Oan anyone work on the Krelman? Of course. Most bee jobs are small ones. But bees know that every small job, if it's"
            + "done well, means a lot. But choose carefully because you'll stay in the job you pick for the rest of your life. The same job the rest of your life? I didn't know that. What's"
            + "the difference? You'll be happy to know that bees, as a species, haven't had one day off in 27 million years. So you'll just work us to death? We'll sure try. Wow! That blew my mind!"
            + "How can you say that? One job forever? That's an insane choice to have to make. I'm relieved. Now we only have to make one decision in life. But, Adam, how could they never have told");
            addItem("longTestNote", note2);
            Lore note3 = new Lore("Broken Note", "This note i broken into mutiple pages", new string[4]{"This is on page 1!", "This is on the second page!!", "This is on the third page!!!", "This is the final page of the book"});
            addItem("testMutiNote", note3);
        }
        initDB = true;
    }

    // Add the item the dictionary
    // @Parms string name : The name to store the item by
    // @Parms Item item : Item to add
    private static void addItem(string name, Item item) {
        itemDB.Add(name, item);
    }

    // Get an item from the DB
    // @Parms string name : The name to search by
    // @Return Item : the item from the DB, null if none is found
    // @Throws System.Exception : Will throw a System.Exception if the DB has not been init
    public static Item getItem(string name) {
        if (!initDB) {
            throw new System.Exception("Error: The ItemDB has not been initlized!");
        }
        Item temp = null;
        itemDB.TryGetValue(name, out temp);
        return temp;
    }
}
