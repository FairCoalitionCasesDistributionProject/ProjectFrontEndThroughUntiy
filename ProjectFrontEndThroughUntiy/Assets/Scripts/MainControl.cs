using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainControl : MonoBehaviour
{
    public static string currentName = "";
    public static int currentIdentifier=0;
    public enum parties
    {
        likud = 0,
        haavoda = 1,
        hareshimaHamshutefet = 2,
        hareshimaHaaravitHameshutefet = 3,
        hazionutHadatit = 4,
        israelBeitenu = 5,
        kaholLavan = 6,
        meretz = 7,
        shas = 8,
        tikvaHadasha = 9,
        yahadutHatora = 10,
        yemina = 11,
        yeshAtid = 12,
    }
    public enum cases
    {
        misradRoshHamemshala = 0,
        misradLebitahonPnim = 1,
        misradLehaganatHasviva = 3,
        misradLepituahHaperiferiaNegevVehagalil = 4,
        misradLeshivyonHevrati = 5,
        misradLeshiruteiDat = 6,
        misradLeshitufPeulaEzori = 7,
        zroaHaavoda = 8,
        misradHaotzar = 9,
        misradHaenergiya = 10,
        misradHabitahon = 11,
        misradHabinuyVehashikun = 12,
        misradHabriut = 13,
        misradHahutz = 14,
        misradHahinuh = 15,
        misradHahaklautVepituahHakfar = 16,
        misradHakalkalaVehataasiya = 17,
        misradHahadshanutHamadaVehatechnologiya = 18,
        misradHamodin = 19,
        misradHamishpatim = 20,
        misradHarevahaVehabitahonHahevrati = 21,
        misradHaaliyaVehaklita = 21,
        misradHapnim = 22,
        misradHatahburaVehabetihutBadrahim = 23,
        misradHatayarut = 24,
        misradHatfutzot = 25,
        misradHatikshoret = 26,
        misradHatarbutVehasport = 27,
        misradJerusalemVemoreshet = 28,
        misradHahityashvut = 29,
    }
    public static int[,] partyParameters = new int[13, 30];
    public static string[] partyNames = new string[13] { "likud", "haavoda", "hareshimaHamshutefet", "hareshimaHaaravitHameshutefet", "hazionutHadatit", "israelBeitenu", "kaholLavan", "meretz", "shas", "tikvaHadasha", "yahadutHatora", "yemina", "yeshAtid" };
    public static string[] casesNamesTranslation = new string[30] { "משרד ראש הממשלה", "המשרד לביטחון הפנים", "המשרד להגנת הסביבה", "המשרד לפיתוח הפריפריה, הנגב והגליל", "המשרד לשוויון חברתי", "המשרד לשירותי דת", "המשרד לשיתוף פעולה אזורי", "זרוע העבודה", "משרד האוצר", "משרד האנרגיה", "משרד הביטחון", "משרד הבינוי והשיכון", "משרד הבריאות", "משרד החוץ", "משרד החינוך", "משרד החקלאות ופיתוח הכפר", "משרד הכלכלה והתעשייה", "משרד החדשנות, המדע והטכנולוגיה", "משרד המודיעין", "משרד המשפטים", "משרד הרווחה והביטחון החברתי", "משרד העלייה והקליטה", "משרד הפנים", "משרד התחבורה והבטיחות בדרכים", "משרד התיירות", "משרד התפוצות", "משרד התקשורת", "משרד התרבות והספורט", "משרד ירושלים ומורשת", "משרד ההתיישבות" };

}





