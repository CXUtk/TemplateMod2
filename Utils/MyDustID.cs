using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 命名空间自己改，这个文件是四十九落星内部源码的一部分
/// </summary>
namespace TemplateMod2.Utils {
    /// <summary>
    /// DXTsT自制的粒子ID表
    /// 制作时间：2017/1/31
    /// 版权所有：DXTsT & 四十九落星制作组
    /// 
    /// 说明：以下字段带有（!）标识符的说明此粒子效果会在黑暗中自发光
    /// 带有（.）标识符说明此粒子效果会高亮显示但是不会发光
    /// 其余Dust全部都不会发光！
    /// 
    /// 感谢所有参与制作者：Rainbow Fluorescence，子冀，海琴烟
    /// </summary>
    public static class MyDustId {
        /// <summary>
        /// 土壤粒子，不发光，受重力影响。
        /// </summary>
        public const int BrownDirt = 0;
        /// <summary>
        /// 岩石粒子，不发光，受重力影响。
        /// </summary>
        public const int GreyStone = 1;
        /// <summary>
        /// 浅绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int GreenGrass = 2;
        /// <summary>
        /// 绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinGreenGrass = 3;
        /// <summary>
        /// 灰色粒子，不发光，受重力影响。
        /// </summary>
        public const int GreyPebble = 4;
        /// <summary>
        /// 深红色粒子，不发光，受重力影响。
        /// </summary>
        public const int RedBlood = 5;
        /// <summary>
        ///（!）橘黄色火焰粒子，自发光，范围中等，受重力影响。
        /// </summary>
        public const int Fire = 6;
        /// <summary>
        /// 深土壤色粒子，不发光，受重力影响。
        /// </summary>
        public const int Wood = 7;
        /// <summary>
        /// 铁矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int PurpleGems = 8;
        /// <summary>
        /// 铜矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int OrangeGems = 9;
        /// <summary>
        /// 金矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int YellowGems = 10;
        /// <summary>
        /// 银矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int WhiteGems = 11;
        /// <summary>
        /// 精金矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int RedGems = 12;
        /// <summary>
        /// 钴蓝矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int CyanGems = 13;
        /// <summary>
        /// 魔晶矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int CorruptionParticle = 14;
        /// <summary>
        ///（!）冰晶色粒子，自发光，范围大，不受重力影响且在重力下持续时间变长。
        /// </summary>
        public const int BlueMagic = 15;
        /// <summary>
        ///（.）浅蓝云色粒高亮，不受重力影响且在重力下持续时间变长。
        /// </summary>
        public const int WhiteClouds = 16;
        /// <summary>
        /// 蓝黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinGrey = 17;
        /// <summary>
        /// 叶绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int SicklyGreen = 18;
        /// <summary>
        /// 金色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinYellow = 19;
        /// <summary>
        /// （!）纯白色粒子，自发光，范围中等，不受重力影响且在重力下持续时间变长。
        /// </summary>
        public const int WhiteLingering = 20;
        /// <summary>
        /// （!）亮粉色粒子，自发光，范围小，不受重力影响且在重力下持续时间变长。
        /// </summary>
        public const int PurpleLingering = 21;
        /// <summary>
        /// 深土壤色粒子，不发光，受重力影响。
        /// </summary>
        public const int Brown = 22;
        /// <summary>
        /// 微粉土壤色粒子，不发光，受重力影响。
        /// </summary>
        public const int Orange = 23;
        /// <summary>
        /// 微紫土壤色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinBrown = 24;
        /// <summary>
        /// 狱岩石色粒子，不发光，受重力影响。
        /// </summary>
        public const int Copper = 25;
        /// <summary>
        /// 枯草色粒子，不发光，受重力影响。
        /// </summary>
        public const int Iron = 26;
        /// <summary>
        /// （!）蓝紫粉色粒子，自发光，范围中等，不受重力影响且在重力下持续时间变长。
        /// </summary>
        public const int PurpleLight = 27;
        /// <summary>
        /// 深铜色粒子，不发光，受重力影响。
        /// </summary>
        public const int DullCopper = 28;
        /// <summary>
        /// （!）深蓝色粒子，自发光，受重力影响。
        /// </summary>
        public const int DarkBluePinkLight = 29;
        /// <summary>
        /// 银白色粒子，不发光，受重力影响。
        /// </summary>
        public const int Silver = 30;
        /// <summary>
        /// 白云色粒子，不发光，不受重力影响。
        /// </summary>
        public const int Smoke = 31;
        /// <summary>
        /// 深黄色粒子，不发光，受重力影响。
        /// </summary>
        public const int Sand = 32;
        /// <summary>
        /// 水蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int Water = 33;
        /// <summary>
        ///（!）金黄火焰色粒子，自发光，范围中等，在重力下不显现，在无重力下显现。
        /// </summary>
        public const int RedLight = 35;
        /// <summary>
        /// 浅黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int MuddyPale = 36;
        /// <summary>
        /// 深蓝黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int DarkGrey = 37;
        /// <summary>
        /// 深土壤色粒子，不发光，受重力影响。
        /// </summary>
        public const int MuddyBrown = 38;
        /// <summary>
        /// 绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int JungleGrass = 39;
        /// <summary>
        /// 深叶绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinGrass = 40;
        /// <summary>
        /// （!）亮水蓝色粒子，自发光，范围大，在重力下停留扩散且时间较长，无重力时消散较快。
        /// </summary>
        public const int BlueCircle = 41;
        /// <summary>
        /// 深钴蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinTeal = 42;
        /// <summary>
        /// （!）亮光色粒子，不稳定发光，光源不穿透，光照范围与大小成正比，不受重力影响。
        /// </summary>
        public const int WhiteLight = 43;
        /// <summary>
        /// （!）黄绿色粒子，发白光，范围很大，在重力下停留扩散且时间较长，无重力时消散较快。
        /// </summary>
        public const int GreenSpores = 44;
        /// <summary>
        /// （!）深水蓝色粒子，自发光，在重力下停留扩散且时间较长，无重力时消散较快。
        /// </summary>
        public const int LightBlueCircle = 45;
        /// <summary>
        /// 深绿色粒子，不发光，不受重力影响。
        /// </summary>
        public const int GreenMaterial = 46;
        /// <summary>
        /// 深蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int CyanGrass = 47;
        /// <summary>
        /// 蘑菇矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlueMushroom = 48;
        /// <summary>
        /// 蓝偏黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlueDrakParticle = 49;
        /// <summary>
        /// 深精金矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int RedParticle = 50;
        /// <summary>
        /// 白土色粒子，不发光，受重力影响。
        /// </summary>
        public const int PearlStone = 51;
        /// <summary>
        /// 粉，水，不发光，浅，色，受，力，高度。明。
        /// </summary>
        public const int PinkWater = 52;
        /// <summary>
        /// 灰色材质，不发光，浅深灰色，受重力，不受重力消失快。
        /// </summary>
        public const int GreyMaterial = 53;
        /// <summary>
        /// 黑色材质，不发光，浅黑色，受重力，不受重力消失快。
        /// </summary>
        public const int BlackMaterial = 54;
        /// <summary>
        /// （!）亮金火焰色粒子，自发光，范围较大，受重力时旋转扩散。
        /// </summary>
        public const int OrangeFx = 55;
        /// <summary>
        /// （!）天蓝色粒子，自发光，范围中等，在重力下扩散，消散较快。
        /// </summary>
        public const int CyanFx = 56;
        /// <summary>
        /// （!）小型黄色神圣特效，自发光，发黄浅浅黄色、金色，受重力时旋转扩散。
        /// </summary>
        public const int YellowHallowFx = 57;
        /// <summary>
        /// （!）亮粉白色粒子，自发光，范围较大，不受重力影响。
        /// </summary>
        public const int PinkMagic = 58;
        /// <summary>
        /// （!）晶蓝色粒子，自发光，高蓝光，受重力影响。
        /// </summary>
        public const int BlueTorch = 59;
        /// <summary>
        /// （!）偏粉红色粒子，自发光，高红光，受重力影响。
        /// </summary>
        public const int RedTorch = 60;
        /// <summary>
        /// （!）亮绿色粒子，自发光，高绿光，受重力影响。
        /// </summary>
        public const int GreenTorch = 61;
        /// <summary>
        /// （!）紫色粒子，自发光，高紫光，受重力影响。
        /// </summary>
        public const int PurpleTorch = 62;
        /// <summary>
        /// (!)灰白色粒子，自发光，白光，受重力影响。
        /// </summary>
        public const int WhiteTorch = 63;
        /// <summary>
        /// (!)纯金色粒子，自发光，受重力影响。
        /// </summary>
        public const int YellowTorch = 64;
        /// <summary>
        /// (!)深紫色粒子，自发光，受重力影响。
        /// </summary>
        public const int DemonTorch = 65;
        /// <summary>
        /// (!)白色粒子，自发光，范围非常大，在重力下迅速变大并且旋转，无重力时消散较快。
        /// </summary>
        public const int WhiteTransparent = 66;
        /// <summary>
        /// (!)浅海蓝色粒子，自发光，范围中等，受重力影响。
        /// </summary>
        public const int CyanIce = 67;
        /// <summary>
        /// (!)暗蓝冰晶，自发光，受重力时发暗蓝光深蓝色，不受重力时高亮发蓝光亮青色。
        /// </summary>
        public const int DarkCyanIce = 68;
        /// <summary>
        /// 粉色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinPink = 69;
        /// <summary>
        /// (!)透明紫色粒子，自发光，受重力时发暗紫光深紫色，不受重力时高亮发粉光亮粉色。
        /// </summary>
        public const int TransparentPurple = 70;
        /// <summary>
        /// (.)透明粉色特效，高亮，发粉浅亮粉色，受重力时旋转扩散。
        /// </summary>
        public const int TransparentPinkFx = 71;
        /// <summary>
        /// (.)红粉色粒子，高亮，在重力下扩散并消散，无重力时消散较快。
        /// </summary>
        public const int SolidPinkFx = 72;
        /// <summary>
        /// (!)亮红粉色粒子，自发光，范围中等，在重力下扩散并消散，无重力时消散较快。
        /// </summary>
        public const int BrightPinkFx = 73;
        /// <summary>
        /// (!)亮绿色粒子，自发光，范围中等，在重力下扩散并消散，无重力时消散较快。
        /// </summary>
        public const int BrightGreenFx = 74;
        /// <summary>
        /// (!)诅咒火把，自发光，发黄绿浅黄绿色，受重力。
        /// </summary>
        public const int CursedFire = 75;
        /// <summary>
        /// 下雪，浅白色，受重力时旋转大范围扩散，存在时间长，遇到物块消失。
        /// </summary>
        public const int Snow = 76;
        /// <summary>
        /// 阴影木，不发光，浅深灰色，受重力，不受重力消失快。
        /// </summary>
        public const int ThinGrey1 = 77;
        /// <summary>
        /// 红木，不发光，浅红棕色，受重力，不受重力消失快。
        /// </summary>
        public const int ThinCopper = 78;
        /// <summary>
        /// 薄黄材质，不发光，浅黄色，受重力下落不旋转，不受重力消失快。
        /// </summary>
        public const int ThinYellow1 = 79;
        /// <summary>
        /// 冰块，不发光，浅蓝白色，受重力，不受重力消失快。
        /// </summary>
        public const int IceBlock = 80;
        /// <summary>
        /// 锡矿石，不发光，浅灰色，受重力，不受重力消失快。
        /// </summary>
        public const int Tin = 81;
        /// <summary>
        /// 铅矿石，不发光，浅蓝黑色，受重力，不受重力消失快。
        /// </summary>
        public const int Lead = 82;
        /// <summary>
        /// 铅矿石，不发光，浅蓝黑色，受重力，不受重力消失快。
        /// </summary>
        public const int Tungsten = 83;
        /// <summary>
        /// 铂矿石，不发光，浅蓝银色，受重力，不受重力消失快。
        /// </summary>
        public const int Platinum = 84;
        /// <summary>
        /// 沙褐材质，不发光，浅橙色，受重力，不受重力消失快。
        /// </summary>
        public const int ThinSandy = 85;
        /// <summary>
        /// (!)少女粉色粒子，自发光，范围较大，受重力影响。
        /// </summary>
        public const int PinkTrans = 86;
        /// <summary>
        /// (!)亮金黄色粒子，自发光，范围中等，受重力影响。
        /// </summary>
        public const int YellowTrans = 87;
        /// <summary>
        /// (!)白偏浅蓝色粒子，自发光，范围中等，受重力影响。
        /// </summary>
        public const int BlueTrans = 88;
        /// <summary>
        /// (!)白绿色粒子，自发光，范围中等，受重力影响。
        /// </summary>
        public const int GreenTrans = 89;
        /// <summary>
        /// (!)粉红色粒子，自发光，范围中等，受重力影响。
        /// </summary>
        public const int RedTrans = 90;
        /// <summary>
        /// (!)亮白色粒子，自发光，范围大，受重力影响。
        /// </summary>
        public const int WhiteTrans = 91;
        /// <summary>
        /// (!)蓝白色粒子，自发光，范围较大，受重力影响。
        /// </summary>
        public const int CyanTrans = 92;
        /// <summary>
        /// 松绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int DarkGrass = 93;
        /// <summary>
        /// 深黄绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int PaleDarkGrass = 94;
        /// <summary>
        /// 深红偏黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int DarkRedGrass = 95;
        /// <summary>
        /// 深蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlackGreenGrass = 96;
        /// <summary>
        /// 深紫偏黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int DarkRedGrass1 = 97;
        /// <summary>
        /// 紫色粒子，不发光，受重力影响。
        /// </summary>
        public const int PurpleWater = 98;
        /// <summary>
        /// 浅绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int CyanWater = 99;
        /// <summary>
        /// 浅粉色粒子，不发光，受重力影响。
        /// </summary>
        public const int PinkWater1 = 100;
        /// <summary>
        /// 浅蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int CyanWater1 = 101;
        /// <summary>
        /// 浅黄土色粒子，不发光，受重力影响。
        /// </summary>
        public const int OrangeWater = 102;
        /// <summary>
        /// 深蓝偏白色粒子，不发光，受重力影响。
        /// </summary>
        public const int DarkBlueWater = 103;
        /// <summary>
        /// 深粉偏黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int HotPinkWater = 104;
        /// <summary>
        /// 大红色粒子，不发光，受重力影响。
        /// </summary>
        public const int RedWater = 105;
        /// <summary>
        /// (.)红黄绿三色火焰色粒子，高亮，受重力影响。
        /// </summary>
        public const int RGBMaterial = 106;
        /// <summary>
        /// (!)亮白绿色粒子，自发光，范围小，在重力下能够悬停较长时间。
        /// </summary>
        public const int GreenFXPowder = 107;
        /// <summary>
        /// 浅灰浅蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int PurpleRound = 108;
        /// <summary>
        /// 黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlackMaterial1 = 109;
        /// <summary>
        /// (.)亮浅蓝偏绿色粒子，高亮，在重力下快速扩散范围但不大。
        /// </summary>
        public const int GreenBubble = 110;
        /// <summary>
        /// (.)亮蓝色粒子，高亮，在重力下快速扩散范围但不大。
        /// </summary>
        public const int CyanBubble = 111;
        /// <summary>
        /// (.)亮粉色粒子，高亮，在重力下快速扩散范围但不大。
        /// </summary>
        public const int PinkBubble = 112;
        /// <summary>
        /// (.)亮深蓝偏白色粒子，高亮，在重力下快速扩散范围但不大。
        /// </summary>
        public const int BlueIce = 113;
        /// <summary>
        /// (.)亮粉偏红色粒子，高亮，在重力下快速扩散范围但不大。
        /// </summary>
        public const int PinkYellowBubble = 114;
        /// <summary>
        /// 锈红色粒子，不发光，受重力影响
        /// </summary>
        public const int RedGrass = 115;
        /// <summary>
        /// 深蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlueGreenGrass = 116;
        /// <summary>
        /// 较锈红色粒子，不发光，受重力影响。
        /// </summary>
        public const int RedGrass1 = 117;
        /// <summary>
        /// 紫蓝白色粒子，不发光，受重力影响。
        /// </summary>
        public const int PurpleGems1 = 118;
        /// <summary>
        /// 深粉红色粒子，不发光，受重力影响。
        /// </summary>
        public const int PinkGems = 119;
        /// <summary>
        /// 深棕白色粒子，不发光，受重力影响。
        /// </summary>
        public const int PalePinkGems = 120;
        /// <summary>
        /// 深灰黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinGrey2 = 121;
        /// <summary>
        /// 深机械色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinIron = 122;
        /// <summary>
        /// 深粉红色粒子，不发光，受重力影响。
        /// </summary>
        public const int HotPinkBubble = 123;
        /// <summary>
        /// 浅黄偏白色粒子，不发光，受重力影响。
        /// </summary>
        public const int YellowWhiteBubble = 124;
        /// <summary>
        /// 深红偏黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinRed = 125;
        /// <summary>
        /// 深灰偏绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinGrey3 = 126;
        /// <summary>
        /// (!)岩浆色粒子，自发光，范围小，受重力影响。
        /// </summary>
        public const int OrangeFire = 127;
        /// <summary>
        /// 叶绿矿色粒子，不发光，受重力影响。
        /// </summary>
        public const int GreenGems = 128;
        /// <summary>
        /// 深土黄色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinBrown1 = 129;
        /// <summary>
        /// (!)白粉色粒子，自发光，范围较大，在重力下会呈现粒子下坠特效，在无重力时会悬停扩散，较快消散。
        /// </summary>
        public const int TrailingPinkWhite = 130;
        /// <summary>
        /// (!)绿色粒子，自发光，重力下扩散范围更大。
        /// </summary>
        public const int TrailingGreen = 131;
        /// <summary>
        /// (!)蓝色粒子，自发光，重力下曳尾。
        /// </summary>
        public const int TrailingCyan = 132;
        /// <summary>
        /// (!)黄色粒子，自发光，重力下曳尾。
        /// </summary>
        public const int TrailingYellow = 133;
        /// <summary>
        /// (!)粉色粒子，自发光，重力下曳尾。
        /// </summary>
        public const int TrailingPink = 134;
        /// <summary>
        /// (!)冰火把，青色粒子, 自发光。
        /// </summary>
        public const int IceTorch = 135;
        /// <summary>
        /// 红色粒子，不发光，受重力影响。
        /// </summary>
        public const int Red = 136;
        /// <summary>
        /// 亮蓝色/青色，不发光受重力影响。
        /// </summary>
        public const int BrightCyan = 137;
        /// <summary>
        /// 亮橙色/棕色，不发光受重力影响。
        /// </summary>
        public const int BrightOrange = 138;
        /// <summary>
        /// 青色纸屑，不发光，受重力缓慢下落。
        /// </summary>
        public const int CyanConfetti = 139;
        /// <summary>
        /// 绿色纸屑，不发光，受重力缓慢下落。
        /// </summary>
        public const int GreenConfetti = 140;
        /// <summary>
        /// 粉色纸屑，不发光，受重力缓慢下落。
        /// </summary>
        public const int PinkConfetti = 141;
        /// <summary>
        /// 黄色纸屑，不发光，受重力缓慢下落。
        /// </summary>
        public const int YellowConfetti = 142;
        /// <summary>
        /// 浅灰色石块粒子，不发光，受重力影响。
        /// </summary>
        public const int LightGreyStone = 143;
        /// <summary>
        /// 铜红色铜砖粒子，不发光，受重力影响。
        /// </summary>
        public const int CopperStone = 144;
        /// <summary>
        /// 粉色石块粒子，不发光，受重力影响。
        /// </summary>
        public const int PinkStone = 145;
        /// <summary>
        /// 钛金砖粒子，不发光，受重力影响。
        /// </summary>
        public const int GreenBrown = 146;
        /// <summary>
        /// 橙色粒子，不发光，受重力影响。
        /// </summary>
        public const int OrangeFx2 = 147;
        /// <summary>
        /// 饱和红色粒子，不发光，受重力影响。
        /// </summary>
        public const int RedDesaturated = 148;
        /// <summary>
        /// 白色粒子，不发光，受重力影响。
        /// </summary>
        public const int White = 149;
        /// <summary>
        /// 黑色/黄色/蓝白粒子，不发光，受重力影响。
        /// </summary>
        public const int BlackYellowBluishwhite = 150;
        /// <summary>
        /// 薄白色材质，不发光，受重力影响。
        /// </summary>
        public const int ThinWhite = 151;
        /// <summary>
        /// 亮橙色粒子，不发光，有重力直接消失，无重力变小到一定程度下落消失。
        /// </summary>
        public const int OrangeBubble = 152;
        /// <summary>
        /// 亮橙色粒子，不发光，受重力影响。
        /// </summary>
        public const int OrangeBubbleMaterial = 153;
        /// <summary>
        /// 薄苍白蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlueThin = 154;
        /// <summary>
        /// 薄暗棕色粒子，不发光，受重力影响。
        /// </summary>
        public const int DarkBrown = 155;
        /// <summary>
        /// (!)亮蓝色/白色粒子，发苍白/蓝色光，高亮显示，受重力影响。
        /// </summary>
        public const int BlueWhiteBubble = 156;
        /// <summary>
        /// (.)薄绿色粒子，不发光，高亮，闪烁，受重力影响。
        /// </summary>
        public const int GreenFx = 157;
        /// <summary>
        /// (!)橙色火焰粒子，发橙色光，受重力影响。
        /// </summary>
        public const int OrangeFire1 = 158;
        /// <summary>
        /// (!)黄色/白色粒子，发黄色光，不受重力影响，无重力消失快。
        /// </summary>
        public const int YellowFx = 159;
        /// <summary>
        /// (!)青色粒子，发亮青色光，不受重力影响，消失快。
        /// </summary>
        public const int CyanShortFx = 160;
        /// <summary>
        /// 青色粒子，不发光，受重力影响。
        /// </summary>
        public const int CyanMaterial = 161;
        /// <summary>
        /// (!)橙色火焰粒子，发橙色光，不受重力影响，消失快。
        /// </summary>
        public const int OrangeShortFx = 162;
        /// <summary>
        /// (.)亮绿色粒子，不发光，高亮，受重力影响。
        /// </summary>
        public const int BrightGreen = 163;
        /// <summary>
        /// (!)粉色粒子，发桃红色光，不受重力影响，无重力消失快。
        /// </summary>
        public const int PinkFx = 164;
        /// <summary>
        /// 白色/蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int WhiteBlueBubble = 165;
        /// <summary>
        /// 薄亮粉色粒子，不发光，受重力影响。
        /// </summary>
        public const int PinkThinBright = 166;
        /// <summary>
        /// 薄绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinGreen = 167;
        /// <summary>
        /// (!)亮粉色粒子，发桃红色光，无重力变大。
        /// </summary>
        public const int PinkBrightBubble = 168;
        /// <summary>
        /// (!)发光黄色粒子, 发深黄色光，受重力影响，消失快。
        /// </summary>
        public const int YellowFx1 = 169;
        /// <summary>
        /// (!)薄橙色特效, 发微弱白色光，受重力影响。
        /// </summary>
        public const int Ichor = 170;
        /// <summary>
        /// 亮紫色粒子，不发光，受重力影响。
        /// </summary>
        public const int PurpleBubble = 171;
        /// <summary>
        /// (!)浅蓝色，发极弱蓝光，高亮，受重力影响。
        /// </summary>
        public const int BlueParticle = 172;
        /// <summary>
        /// (!)亮紫色粒子，发紫色光，不受重力影响，消失快。
        /// </summary>
        public const int PurpleShortFx = 173;
        /// <summary>
        /// (!)亮橙色粒子，发橙红色光，受重力影响。
        /// </summary>
        public const int OrangeFire2 = 174;
        /// <summary>
        /// (.)白色粒子，不发光，高亮，不受重力影响，消失快。
        /// </summary>
        public const int WhiteShortFx = 175;
        /// <summary>
        /// 浅蓝色粒子，不发光，受重力影响。
        /// </summary>
        public const int LightBlueParticle = 176;
        /// <summary>
        /// 浅粉色粒子，不发光，受重力影响。
        /// </summary>
        public const int LightPinkParticle = 177;
        /// <summary>
        /// 浅绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int LightGreenParticle = 178;
        /// <summary>
        /// 浅紫色粒子，不发光，受重力影响。
        /// </summary>
        public const int LightPurpleParticle = 179;
        /// <summary>
        /// (.)发光浅青粒子，不发光，高亮，受重力影响。
        /// </summary>
        public const int LightCyanParticle = 180;
        /// <summary>
        /// (.)浅青色/粉色粒子，不发光，高亮，受重力影响。
        /// </summary>
        public const int CyanPinkBubble = 181;
        /// <summary>
        /// (.)浅红色粒子，不发光，高亮，受重力影响。
        /// </summary>
        public const int RedBubble = 182;
        /// <summary>
        /// (.)半透明红色粒子，不发光，高亮，受重力影响。
        /// </summary>
        public const int RedTransBubble = 183;
        /// <summary>
        /// 枯绿色/绿灰色粒子，不发光，不受重力影响。
        /// </summary>
        public const int GreenishGreyParticle = 184;
        /// <summary>
        /// (!)浅青色粒子，发青色光，受重力影响。
        /// </summary>
        public const int CyanCrystal = 185;
        /// <summary>
        /// 暗蓝色粒子，不发光，不受重力影响。
        /// </summary>
        public const int DarkBlueSmoke = 186;
        /// <summary>
        /// (!)浅青色粒子，发青色光，受重力影响，消失快。
        /// </summary>
        public const int LightCyanParticle1 = 187;
        /// <summary>
        /// 亮绿色粒子，不发光，不受重力影响。
        /// </summary>
        public const int GreenBubble1 = 188;
        /// <summary>
        /// 薄橙色粒子，不发光，受重力影响。
        /// </summary>
        public const int OrangeMaterial = 189;
        /// <summary>
        /// 薄金色粒子，不发光，受重力影响。
        /// </summary>
        public const int GoldMaterial = 190;
        /// <summary>
        /// 黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlackFlakes = 191;
        /// <summary>
        /// 雪白色粒子，不发光，受重力影响。
        /// </summary>
        public const int SnowMaterial = 192;
        /// <summary>
        /// 绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int GreenMaterial1 = 193;
        /// <summary>
        /// 薄棕色粒子，不发光，受重力影响。
        /// </summary>
        public const int BrownMaterial = 194;
        /// <summary>
        /// 薄黑色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlackMaterial2 = 195;
        /// <summary>
        /// 薄绿色粒子，不发光，受重力影响。
        /// </summary>
        public const int ThinGreen1 = 196;
        /// <summary>
        /// (.)薄亮青色粒子，不发光，高亮，受重力影响。
        /// </summary>
        public const int BrightCyanMaterial = 197;
        /// <summary>
        /// 黑色/白色粒子，不发光，受重力影响。
        /// </summary>
        public const int BlackWhiteParticle = 198;
        /// <summary>
        /// 黑色/灰色粒子，不发光，受重力影响。
        /// </summary>
        public const int PurpleBlackGrey = 199;
        /// <summary>
        /// 橙色粒子，不发光，受重力影响。
        /// </summary>
        public const int PinkParticle = 200;
        /// <summary>
        /// 浅橙色粒子，不发光，受重力影响。
        /// </summary>
        public const int LightPinkParticle1 = 201;
        /// <summary>
        /// 浅青色粒子，不发光，受重力影响。
        /// </summary>
        public const int LightCyanParticle2 = 202;
        /// <summary>
        /// 灰色粒子，不发光，受重力影响。
        /// </summary>
        public const int GreyParticle = 203;
        /// <summary>
        /// (.)白色/黄色/橙黄色粒子，不发光，高亮，受重力影响。
        /// </summary>
        public const int WhiteParticle = 204;
        /// <summary>
        /// (!)薄粉色材质粒子，几乎不发粉色光，高亮，受重力影响。
        /// </summary>
        public const int ThinPinkMaterial = 205;
        /// <summary>
        /// (!)青色粒子，发很暗的蓝色光，不受重力影响，消失快。
        /// </summary>
        public const int CyanShortFx1 = 206;//从这开始
        /// <summary>
        /// 薄棕色材质，不发光，受重力。
        /// </summary>
        public const int BrownMaterial1 = 207;
        /// <summary>
        /// 橙色石块，不发光，受重力。
        /// </summary>
        public const int OrangeStone = 208;
        /// <summary>
        /// 苍青色石块，不发光，受重力。
        /// </summary>
        public const int PaleGreenStone = 209;
        /// <summary>
        /// 白色粒子，不发光，受重力。
        /// </summary>
        public const int OffWhite = 210;
        /// <summary>
        /// 亮蓝色粒子，不发光，受重力。
        /// </summary>
        public const int BrightBlueParticle = 211;
        /// <summary>
        /// 白色粒子，不发光，受重力。
        /// </summary>
        public const int WhiteParticle1 = 212;
        /// <summary>
        /// (!)黄色粒子，发很暗的黄光，高亮，受重力影响，极微小且消失快。
        /// </summary>
        public const int WhiteShortFx1 = 213;
        /// <summary>
        /// 薄苍棕色粒子，不发光，受重力。
        /// </summary>
        public const int Thin = 214;
        /// <summary>
        /// 薄棕色粒子，不发光，受重力。
        /// </summary>
        public const int ThinKhaki = 215;
        /// <summary>
        /// 苍粉色粒子，不发光，受重力。
        /// </summary>
        public const int Pale = 216;
        /// <summary>
        /// 青色粒子，不发光，受重力。
        /// </summary>
        public const int Cyan = 217;
        /// <summary>
        /// 红色粒子，不发光，受重力。
        /// </summary>
        public const int Hot = 218;
        /// <summary>
        /// (!)红色粒子，发橙色光，重力下曳尾。
        /// </summary>
        public const int TrailingRed1 = 219;
        /// <summary>
        /// (!)亮绿色粒子，发亮绿色光，重力下曳尾。
        /// </summary>
        public const int TrailingGreen1 = 220;
        /// <summary>
        /// (!)蓝色粒子，发蓝色光，重力下曳尾。
        /// </summary>
        public const int TrailingBlue = 221;
        /// <summary>
        /// (!)黄色粒子，发黄色光，重力下曳尾。
        /// </summary>
        public const int TrailingYellow1 = 222;
        /// <summary>
        /// (.)粉色粒子，不发光，高亮，重力下曳尾。
        /// </summary>
        public const int TrailingRed2 = 223;
        /// <summary>
        /// 薄蓝色材质，不发光，受重力。
        /// </summary>
        public const int ThinBlue = 224;
        /// <summary>
        /// 橙色材质，不发光，受重力。
        /// </summary>
        public const int OrangeMaterial1 = 225;
        /// <summary>
        /// (!)亮浅蓝色粒子，发亮浅蓝色光，高亮，受重力。
        /// </summary>
        public const int ElectricCyan = 226;
        /// <summary>
        /// 亮紫色粒子，不发光，受重力影响，高速旋转，无重力消散较快
        /// </summary>
        public const int PurpleParticle1 = 227;
        /// <summary>
        /// (!)亮黄色+白色粒子，发黄色光，受重力影响
        /// </summary>
        public const int YellowGoldenFire = 228;
        /// <summary>
        /// (!)发光月炎 火焰!!! 重力下高速旋转
        /// </summary>
        public const int CyanLunarFire = 229;
        /// <summary>
        /// (!)类似月炎，但是照明范围较大，重力下高速旋转
        /// </summary>
        public const int CyanLunarFire1 = 230;
        /// <summary>
        /// (!)橙红色粒子，发橙色光，重力下消散时间较长
        /// </summary>
        public const int OrangeFx3 = 231;
        /// <summary>
        /// 屎黄色粒子，不发光，受重力影响
        /// </summary>
        public const int YellowMaterial1 = 232;
        /// <summary>
        /// 黄色+屎黄色粒子，不发光，受重力影响
        /// </summary>
        public const int YellowMaterial2 = 233;
        /// <summary>
        /// (!)淡紫色粒子，发出紫色光，受重力影响
        /// </summary>
        public const int PurpleHighFx = 234;
        /// <summary>
        /// (.)血红色高亮粒子，半透明，不受重力影响
        /// </summary>
        public const int RedBlood1 = 234;
        /// <summary>
        /// 银白色粒子，不发光，受重力影响
        /// </summary>
        public const int SilverMaterial = 234;
    }
}
