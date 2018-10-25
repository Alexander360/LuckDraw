# LuckDraw
WPF做的大转盘程序<br />
企划部抽奖程序<br />
可以配置抽奖逻辑，按时间段出奖<br />
输入收银机编号，订单号 和金额符合条件即可抽奖 <br />
生成的目录下的config 文件 <br />
        Jackpot.xml 是抽奖规则（具体逻辑可以自行分析很easy）<br />
        user.xml  是抽奖人的信息<br />
app.config 中如果admin 为1 则启动一个统计信息的窗体，不为1 则启动抽奖界面<br />
App.xaml.cs 是启动逻辑<br />
JackPotLogic 是抽奖的准备工作<br />
LiuxuRandom  是抽奖的具体逻辑<br />
image文件夹下是需要用到的图片（背景图 大转盘图）<br />

