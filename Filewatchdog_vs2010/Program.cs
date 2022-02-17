/*
 作者:zhw
 版本说明:
 V1.0  具备数据读取功能
 V1.1  基础分析数据获取
 V1.2  将实时数据与分析数据分开
 V1.3  增加一阶斜率过滤20211101
 V1.4  增加兑铁结束时刻捕捉20211102
 V1.5  优化兑铁开始、兑铁结束参数 增加一阶低通滤波 
 V1.6  优化兑铁结束参数，一阶低通滤波系数改完50；增加异常捕获
 V1.7  增加兑铁位置数据
 V1.8  增加监控文件过滤"*LB.LOG"
 V1.9  将位置检测加入连锁，只有兑铁位才进行数据存储 42744 、 51440
 * 
 *
 * 
 * 
 * 
 * 当前版本V1.6
 */





using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Filewatchdog_vs2010
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "FileWatchDog_V1.9";
                //初始化监控器
                //FileWatcher watcher = new FileWatcher(@"F:\learn\C#\Filewatchdog_vs2010\数据");
                FileWatcher watcher = new FileWatcher(@"C:\新建文件夹\log\DataBee_Realtime","*LB.LOG");



                watcher.Open();
         

//*****************调试专用
                var t1 = new Thread(new ThreadStart(watcher.Data_Save_Process)); //文件监控，向内存写入数据
                t1.Start();

               
                while (true)
                {
                    //如果IsWatch为False，则可能监控内部发生异常终止了监控，需要重新开启监控
                    if (watcher.IsWatch)
                    {
                        //队列顶端的变更消息
                        //  fci = watcher.Get();
                        //处理消息的代码
                        //Print(fci);
                    }
                    else
                    {
                        watcher.Open();
                    }

               


                    Thread.Sleep(5000);

                }


            }
            catch(Exception E)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("系统报错：",E.Message.ToString());
            }



        }
    }


    /// <summary>
    /// 文件监控类，用于监控指定目录下文件以及文件夹的变化
    /// </summary>
    public class FileWatcher
    {
        private System.IO.FileSystemWatcher _watcher = null;
        private string _path = string.Empty;
        private string _filter = string.Empty;
        private bool _isWatch = false;
        // private CustomQueue<FileChangeInformation> _queue = null;
        private sql_data sql_db = new sql_data();
        private float temp_weight_162 = 0;
        private float temp_weight_161 = 0;

        private float avrg = 0;
        private float Variance = 0;
        private int count = 0;


        //阈值控制162/161 兑铁开始
        private float Variance_limit_low_begin = 5.0F;
        private float Variance_limit_high_begin = 30.0F;

        private float weight_limit_begin = 280.0F;
        private float Slope1_5_lower_begin = -30.0F;
        private float Slope1_5_high_begin = -5.0F;

        private float AVG_lower_begin = 280.0F;
        private float AVG_high_begin = 330.0F;

        private float Slope1_1_lower_begin = -20.0F;
        private float Slope1_1_high_begin = 1.0F;


        //阈值控制162/161 兑铁结束
        private float Variance_limit_low_end = 0.0F;
        private float Variance_limit_high_end = 5.0F;

        private float weight_limit_end = 95.0F;

        private float Slope1_3_lower_end = -1.0F;
        private float Slope1_3_high_end = 10.0F;
        private float Slope1_5_lower_end = 0.0F;
        private float Slope1_5_high_end = 10.0F;

        private float AVG_lower_end = 90.0F;
        private float AVG_high_end = 150.0F;

        private float Slope1_1_lower_end = -1.0F;
        private float Slope1_1_high_end = 20.0F;



        /// <summary>
        /// 低通滤波器
        /// </summary>
        private float RC_K_value = 45;///低通滤波器K值   1-100；
        private float RC_Threshold_value = 50;///threshold  阈值；  
        private List<float> RC_Value_array_161_3 = new List<float>();
        private List<float> RC_Value_array_162_3 = new List<float>();










        private Dictionary<string, float> Runtime_ChargeData_Dic_161 = new Dictionary<string,float>();//兑铁数据（time，weight）
        private Dictionary<string, float> Runtime_ChargeData_Dic_162 = new Dictionary<string, float>();//兑铁数据（time，weight）

        private int ChargeData_Time_161=0;
        private int ChargeData_Time_162 = 0;





        //数值数组
        private List<float> Runtime_Value_array_161 = new List<float>();
        private List<float> Runtime_Value_array_162 =new List<float>();
        private List<float> Runtime_Value_array_161_5 = new List<float>();
        private List<float> Runtime_Value_array_162_5 = new List<float>();
        private List<float> Runtime_Value_array_161_3 = new List<float>();
        private List<float> Runtime_Value_array_162_3 = new List<float>();
        //方差数组
        //private float[] Variance_value;
        Queue<float> Variance_value_queue_161_10 = new Queue<float>();
        Queue<float> Variance_value_queue_162_10 = new Queue<float>();
        Queue<float> Variance_value_queue_161_5 = new Queue<float>();
        Queue<float> Variance_value_queue_162_5 = new Queue<float>();
        Queue<float> Variance_value_queue_161_3 = new Queue<float>();
        Queue<float> Variance_value_queue_162_3 = new Queue<float>();

        //一阶斜率
        //private float[] Variance_value;
        List<float> Slope1_value_Array_161_5 = new List<float>();
        List<float> Slope1_value_Array_162_5 = new List<float>();
        float Slope1_Sum_queue_161_5 = 0;
        float Slope1_Sum_queue_162_5 =0;

        List<float> Slope1_value_Array_161_3 = new List<float>();
        List<float> Slope1_value_Array_162_3 = new List<float>();
        float Slope1_Sum_queue_161_3= 0;
        float Slope1_Sum_queue_162_3 = 0;

        //二阶斜率
        float Slope2_value_Array_161_1 =0;
        float Slope2_value_Array_162_1 =0;

        //平均数
        List<float> AVG_Value_Array_161_1 = new List<float>();
        List<float> AVG_Value_Array_162_1 = new List<float>();


        ////二阶斜率
        ////private float[] Variance_value;
        //List<float> Slope2_value_queue = new List<float>();


        float Crane_Weight_Curent_161 = 0;
        string datatime_str_current_161 = "";
        float Axis_X_161 = 0;
        float Axis_Y_161 = 0;

        


        float Crane_Weight_Curent_162 = 0;
        string datatime_str_current_162 = "";
        float Axis_X_162 = 0;
        float Axis_Y_162 = 0;







        /// <summary>
        /// 监控是否正在运行
        /// </summary>
        public bool IsWatch
        {
            get
            {
                return _isWatch;
            }
        }

        /// <summary>
        /// 文件变更信息队列
        /// </summary>
        //public CustomQueue<FileChangeInformation> FileChangeQueue
        //{
        //    get
        //    {
        //        return _queue;
        //    }
        //}

        /// <summary>
        /// 初始化FileWatcher类
        /// </summary>
        /// <param name="path">监控路径</param>
        public FileWatcher(string path)
        {
            _path = path;
            //_queue = new CustomQueue<FileChangeInformation>();



        }
        /// <summary>
        /// 初始化FileWatcher类，并指定是否持久化文件变更消息
        /// </summary>
        /// <param name="path">监控路径</param>
        /// <param name="isPersistence">是否持久化变更消息</param>
        /// <param name="persistenceFilePath">持久化保存路径</param>
        public FileWatcher(string path, bool isPersistence, string persistenceFilePath)
        {
            _path = path;
            // _queue = new CustomQueue<FileChangeInformation>(isPersistence, persistenceFilePath);
        }

        /// <summary>
        /// 初始化FileWatcher类，并指定是否监控指定类型文件
        /// </summary>
        /// <param name="path">监控路径</param>
        /// <param name="filter">指定类型文件，格式如:*.txt,*.doc,*.rar</param>
        public FileWatcher(string path, string filter)
        {
            _path = path;
            _filter = filter;
            // _queue = new CustomQueue<FileChangeInformation>();
        }

        /// <summary>
        /// 初始化FileWatcher类，并指定是否监控指定类型文件，是否持久化文件变更消息
        /// </summary>
        /// <param name="path">监控路径</param>
        /// <param name="filter">指定类型文件，格式如:*.txt,*.doc,*.rar</param>
        /// <param name="isPersistence">是否持久化变更消息</param>
        /// <param name="persistenceFilePath">持久化保存路径</param>
        public FileWatcher(string path, string filter, bool isPersistence, string persistenceFilePath)
        {
            _path = path;
            _filter = filter;
            // _queue = new CustomQueue<FileChangeInformation>(isPersistence, persistenceFilePath);
        }

        /// <summary>
        /// 打开文件监听器
        /// </summary>
        public void Open()
        {
            if (!System.IO.Directory.Exists(_path))
            {
                System.IO.Directory.CreateDirectory(_path);
            }

            if (string.IsNullOrEmpty(_filter))
            {
                _watcher = new System.IO.FileSystemWatcher(_path);
            }
            else
            {
                _watcher = new FileSystemWatcher(_path, _filter);
            }
            //注册监听事件
            //_watcher.Created += new FileSystemEventHandler(OnProcess);
            _watcher.Changed += new FileSystemEventHandler(OnProcess);

            _watcher.Deleted += new FileSystemEventHandler(OnProcess);
            _watcher.Renamed += new RenamedEventHandler(OnFileRenamed);
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
            _isWatch = true;
        }

        /// <summary>
        /// 关闭监听器
        /// </summary>
        public void Close()
        {
            _isWatch = false;
           // _watcher.Created -= new System.IO.FileSystemEventHandler(OnProcess);
            _watcher.Changed -= new FileSystemEventHandler(OnProcess);

            _watcher.Deleted -= new FileSystemEventHandler(OnProcess);
            _watcher.Renamed -= new RenamedEventHandler(OnFileRenamed);
            _watcher.EnableRaisingEvents = false;
            _watcher = null;
        }

        /// <summary>
        /// 获取一条文件变更消息
        /// </summary>
        /// <returns></returns>
        //public FileChangeInformation Get()
        //{
        //    FileChangeInformation info = null;
        //    if (_queue.Count > 0)
        //    {
        //        lock (_queue)
        //        {
        //            info = _queue.Dequeue();
        //        }
        //    }
        //    return info;
        //}

        /// <summary>
        /// 监听事件触发的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProcess(object sender, FileSystemEventArgs e)
        {
            try
            {
                // FileChangeType changeType = FileChangeType.Unknow;
                if (e.ChangeType == WatcherChangeTypes.Created)
                {
                    Console.WriteLine("创建文件！");
                    if (File.GetAttributes(e.FullPath) == FileAttributes.Directory)
                    {
                        // changeType = FileChangeType.NewFolder;
                    }
                    else
                    {
                        // changeType = FileChangeType.NewFile;
                    }
                }
                else if (e.ChangeType == WatcherChangeTypes.Changed)
                {
                    _watcher.EnableRaisingEvents = false;
               
                    string temp = e.Name.Substring(11, 14);
                  

                    if (temp == "Crane161LB.LOG")
                    {
                        //Console.WriteLine("修改文件！{0},{1}", e.FullPath, e.Name);
                        fileToString_161(e.FullPath);
                    }

                    if (temp == "Crane162LB.LOG")
                    {
                        //Console.WriteLine("修改文件！{0},{1}", e.FullPath, e.Name);
                        fileToString_162(e.FullPath);
                    }

                    //部分文件创建时同样触发文件变化事件，此时记录变化操作没有意义
                    //如果
                    //if (_queue.SelectAll(
                    //    delegate (FileChangeInformation fcm)
                    //    {
                    //        return fcm.NewPath == e.FullPath && fcm.ChangeType == FileChangeType.Change;
                    //    }).Count<FileChangeInformation>() > 0)
                    //{
                    //    return;
                    //}

                    //文件夹的变化，只针对创建，重命名和删除动作，修改不做任何操作。
                    //因为文件夹下任何变化同样会触发文件的修改操作，没有任何意义.
                    //if (File.GetAttributes(e.FullPath) == FileAttributes.Directory)
                    //{
                    //    return;
                    //}

                    // changeType = FileChangeType.Change;
                
                    _watcher.EnableRaisingEvents = true;
                }
                else if (e.ChangeType == WatcherChangeTypes.Deleted)
                {
                    Console.WriteLine("删除文件！");
                    // changeType = FileChangeType.Delete;
                }

                ////创建消息，并压入队列中
                //FileChangeInformation info = new FileChangeInformation(Guid.NewGuid().ToString(), changeType, e.FullPath, e.FullPath, e.Name, e.Name);
                //_queue.Enqueue(info);
            }
            catch
            {
                Close();
            }
        }


        /// <summary>
        /// 文件或目录重命名时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                //创建消息，并压入队列中
                //FileChangeInformation info = new FileChangeInformation(Guid.NewGuid().ToString(), FileChangeType.Rename, e.OldFullPath, e.FullPath, e.OldName, e.Name);
                //_queue.Enqueue(info);
            }
            catch
            {
                Close();
            }
        }


        //低通滤波器计算公式
        private float RC_Cal(float X, float X_sample)   //X 上一次滤波值，X_sample最新取样值
        {
            //if (Math.Abs(X_sample - X) > RC_Threshold_value)
            //{
            //    return X;
            //}
            //else
            {
                return (100 - RC_K_value) * X / 100 + RC_K_value * X_sample / 100;
            }
        }







        //方差计算公式
        private float Variance_Cal(List<float> a)
        {
            float var = 0;
            int n = 0;
            avrg = Average(a);
            n = a.Count;
            foreach (float x in a)
            {
            var += (x - avrg) * (x - avrg);
            }
           // double Variance = var / n;
            return (float)Math.Sqrt(var / n);
        }


        private float Variance_Cal_5(List<float> a)
        {
            float var = 0;
            int n = 0;
            avrg = Average(a);
            n = a.Count;
            foreach (float x in a)
            {
                var += (x - avrg) * (x - avrg);
            }
            // double Variance = var / n;
            return (float)Math.Sqrt(var / n);
        }

        private float Average(List<float> ary)
        {
            float ave_value=0;
            float sum = 0;
            for (int i = 0; i < ary.Count; i++)
            {
                sum += ary[i];
            }
            ave_value = sum / ary.Count;
            return ave_value;
        }
        private float Average_3(List<float> ary)
        {
            float ave_value = 0;
            float sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += ary[i];
            }
            ave_value = sum / 3;
            return ave_value;
        }

        private float Sum_Slope1(List<float> ary)
        {
            float sum = 0;
            for (int i = 0; i < ary.Count; i++)
            {
                sum += ary[i];
            }

            return sum;
        }



        public string fileToString_161(String filePath)
        {
            string strData = "";
            try
            {
                //string line;
                //// 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                //using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                //{

                //    // 从文件读取并显示行，直到文件的末尾
                //    while ((line = sr.ReadLine()) != null)
                //    {
                //        //Console.WriteLine(line);
                //        strData = line;
                //    }
                //}

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fileStream.Seek(-100, SeekOrigin.End);
                    byte[] buf = new byte[100];
                    fileStream.Read(buf, 0, buf.Length);
                    string s = Encoding.ASCII.GetString(buf, 0, buf.Length);
                    string newString = s.Substring(0, s.Length - 2);
                    char[] split = { '\r', '\n' };
                    string[] str = newString.Split(split);
                    char[] split1 = { ',', '\"' };
                    string[] str_data = str[2].Split(split1);
                    datatime_str_current_161 = str_data[13].ToString();

                    Axis_X_161 = (float)Convert.ToDouble(str_data[4].ToString());
                    Axis_Y_161 = (float)Convert.ToDouble(str_data[7].ToString());

                    Crane_Weight_Curent_161 = (float)Convert.ToDouble(str_data[10].ToString());
                }

            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }



        public  string fileToString_162(String filePath)
        {
            string strData = "";
            try
            {
                //string line;
                //// 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                //using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                //{

                //    // 从文件读取并显示行，直到文件的末尾
                //    while ((line = sr.ReadLine()) != null)
                //    {
                //        //Console.WriteLine(line);
                //        strData = line;
                //    }
                //}

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fileStream.Seek(-100, SeekOrigin.End);
                    byte[] buf = new byte[100];
                    fileStream.Read(buf, 0, buf.Length);
                    string s = Encoding.ASCII.GetString(buf, 0, buf.Length);
                    string newString = s.Substring(0, s.Length - 2);
                    char[] split = { '\r', '\n' };
                    string[] str = newString.Split(split);
                    char[] split1 = { ',', '\"' };
                    string[] str_data = str[2].Split(split1);
                    datatime_str_current_162 = str_data[13].ToString();

                    Axis_X_162 = (float)Convert.ToDouble(str_data[4].ToString());
                    Axis_Y_162 = (float)Convert.ToDouble(str_data[7].ToString());

                    Crane_Weight_Curent_162 = (float)Convert.ToDouble(str_data[10].ToString());
                }


            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }


        public void Analysis_Data_Get()
        {
            //161数据加工
            Runtime_Value_array_161.Add(Crane_Weight_Curent_161);
            Runtime_Value_array_161_5.Add(Crane_Weight_Curent_161);
            Runtime_Value_array_161_3.Add(Crane_Weight_Curent_161);


            if (Runtime_Value_array_161.Count > 10)
            {
                Runtime_Value_array_161.RemoveAt(0);
            }
            if (Runtime_Value_array_161_5.Count > 5)
            {
                Runtime_Value_array_161_5.RemoveAt(0);
            }
            if (Runtime_Value_array_161_3.Count > 3)
            {
                Runtime_Value_array_161_3.RemoveAt(0);
            }

            if (Runtime_Value_array_161.Count > 1)
            {
                //10方差计算
                float Var_single = Variance_Cal(Runtime_Value_array_161);
                Variance_value_queue_161_10.Enqueue(Var_single);
                if (Variance_value_queue_161_10.Count > 10)
                {
                    Variance_value_queue_161_10.Dequeue();
                }

                //5方差计算
                float Var_single_5 = Variance_Cal(Runtime_Value_array_161_5);
                Variance_value_queue_161_5.Enqueue(Var_single_5);
                if (Variance_value_queue_161_5.Count > 5)
                {
                    Variance_value_queue_161_5.Dequeue();
                }

                //3方差计算
                float Var_single_3 = Variance_Cal(Runtime_Value_array_161_3);
                Variance_value_queue_161_3.Enqueue(Var_single_3);
                if (Variance_value_queue_161_3.Count > 3)
                {
                    Variance_value_queue_161_3.Dequeue();
                }
            }


            //斜率计算  
            if (Runtime_Value_array_161_3.Count == 3)
            {
                float Slope1_Single = Runtime_Value_array_161_3[2] - Runtime_Value_array_161_3[1];
                Slope1_value_Array_161_5.Add(Slope1_Single);
                if (Slope1_value_Array_161_5.Count > 5)
                {
                    Slope1_value_Array_161_5.RemoveAt(0);
                }
                Slope1_value_Array_161_3.Add(Slope1_Single);
                if (Slope1_value_Array_161_3.Count > 3)
                {
                    Slope1_value_Array_161_3.RemoveAt(0);
                }

                //一阶斜率5求和
                Slope1_Sum_queue_161_5 = Sum_Slope1(Slope1_value_Array_161_5);

                //一阶斜率3求和
                Slope1_Sum_queue_161_3 = Sum_Slope1(Slope1_value_Array_161_3);

                //二阶斜率计算
                if (Slope1_value_Array_161_3.Count ==3)
                {
                    Slope2_value_Array_161_1 = Slope1_value_Array_161_3[2] - Slope1_value_Array_161_3[1];
                }


            }

            //平均数  最近3个的平均数
            if (Runtime_Value_array_161_3.Count == 3)
            {
                float Avg_value_single = Average(Runtime_Value_array_161_3);
                AVG_Value_Array_161_1.Add(Avg_value_single);
                if (AVG_Value_Array_161_1.Count > 1)
                {
                    AVG_Value_Array_161_1.RemoveAt(0);
                }
            }


            //数据过滤
            if (RC_Value_array_161_3.Count < 3)
            {
                RC_Value_array_161_3.Add(Crane_Weight_Curent_161);
                RC_Value_array_161_3.Add(Crane_Weight_Curent_161);
                RC_Value_array_161_3.Add(Crane_Weight_Curent_161);
            }
            if (RC_Value_array_161_3.Count == 3)
            {
                float RC_value_single = RC_Cal(RC_Value_array_161_3[2], Crane_Weight_Curent_161);
                RC_Value_array_161_3.Add(RC_value_single);
            }
            if (RC_Value_array_161_3.Count > 3)
            {
                RC_Value_array_161_3.RemoveAt(0);
            }




////////////////////162数据加工//////////////////////////////////////////////////////////////////////////////////////////////////162数据加工
            Runtime_Value_array_162.Add(Crane_Weight_Curent_162);
            Runtime_Value_array_162_5.Add(Crane_Weight_Curent_162);
            Runtime_Value_array_162_3.Add(Crane_Weight_Curent_162);

            if (Runtime_Value_array_162.Count > 10)
            {
                Runtime_Value_array_162.RemoveAt(0);
            }
            if (Runtime_Value_array_162_5.Count > 5)
            {
                Runtime_Value_array_162_5.RemoveAt(0);
            }
            if (Runtime_Value_array_162_3.Count > 3)
            {
                Runtime_Value_array_162_3.RemoveAt(0);
            }

            if (Runtime_Value_array_162.Count > 1)
            {
                //10方差计算
                float Var_single = Variance_Cal(Runtime_Value_array_162);
                Variance_value_queue_162_10.Enqueue(Var_single);
                if (Variance_value_queue_162_10.Count > 10)
                {
                    Variance_value_queue_162_10.Dequeue();
                }

                //5方差计算
                float Var_single_5 = Variance_Cal(Runtime_Value_array_162_5);
                Variance_value_queue_162_5.Enqueue(Var_single_5);
                if (Variance_value_queue_162_5.Count > 5)
                {
                    Variance_value_queue_162_5.Dequeue();
                }

                //3方差计算
                float Var_single_3 = Variance_Cal(Runtime_Value_array_162_3);
                Variance_value_queue_162_3.Enqueue(Var_single_3);
                if (Variance_value_queue_162_3.Count > 3)
                {
                    Variance_value_queue_162_3.Dequeue();
                }
            }


            //斜率计算  
            if (Runtime_Value_array_162_3.Count == 3)
            {
                float Slope1_Single = Runtime_Value_array_162_3[2] - Runtime_Value_array_162_3[1];

                Slope1_value_Array_162_5.Add(Slope1_Single);
                if (Slope1_value_Array_162_5.Count > 5)
                {
                    Slope1_value_Array_162_5.RemoveAt(0);
                }

                Slope1_value_Array_162_3.Add(Slope1_Single);
                if (Slope1_value_Array_162_3.Count > 3)
                {
                    Slope1_value_Array_162_3.RemoveAt(0);
                }

                //一阶斜率5求和
                Slope1_Sum_queue_162_5 = Sum_Slope1(Slope1_value_Array_162_5);

                //一阶斜率3求和
                Slope1_Sum_queue_162_3 = Sum_Slope1(Slope1_value_Array_162_3);

                //二阶斜率计算/加速度
                if (Slope1_value_Array_162_3.Count == 3)
                {
                    Slope2_value_Array_162_1 = Slope1_value_Array_162_3[2] - Slope1_value_Array_162_3[1];
                }

            }
           

            //平均数  最近3个的平均数
            if (Runtime_Value_array_162_3.Count == 3)
            {
                float Avg_value_single = Average(Runtime_Value_array_162_3);
                AVG_Value_Array_162_1.Add(Avg_value_single);
                if (AVG_Value_Array_162_1.Count > 1)
                {
                    AVG_Value_Array_162_1.RemoveAt(0);
                }
            }


            //数据过滤
            if (RC_Value_array_162_3.Count < 3)
            {
                RC_Value_array_162_3.Add(Crane_Weight_Curent_162);
                RC_Value_array_162_3.Add(Crane_Weight_Curent_162);
                RC_Value_array_162_3.Add(Crane_Weight_Curent_162);
            }
            if (RC_Value_array_162_3.Count ==3)
            {
                float RC_value_single = RC_Cal(RC_Value_array_162_3[2],Crane_Weight_Curent_162);
                RC_Value_array_162_3.Add(RC_value_single);
            }
            if (RC_Value_array_162_3.Count > 3)
            {
                RC_Value_array_162_3.RemoveAt(0);
            }



            //分析数据存储
            try
            {
                //161
                if ((Variance_value_queue_161_10.Count == 10) && (AVG_Value_Array_161_1.Count == 1) && (Variance_value_queue_161_5.Count > 4) && (RC_Value_array_161_3.Count == 3) && ((Axis_X_161 == 51440) || (Axis_X_161 == 42744)))
                {
                    //if (Math.Abs(AVG_Value_Array_161[4] - Crane_Weight_Curent_161) > 1)
                    {
                        //基础数据获取
                        string sql1 = @"insert into dbo.Crane_data_161_Analysis";
                        sql1 += "(crane_id,crane_weight,crane_server_time,Variance_value_10,Variance_value_5,Variance_value_3,Slope1_value_5,Slope1_value_3,Slope2_value_1,AVG_value_3,timestamp,RC_value_1,axis_x,axis_y)  ";
                        sql1 += "  values('161'," + Crane_Weight_Curent_161 + ",'" + datatime_str_current_161 + "'," + Variance_value_queue_161_10.ToArray()[9] + ",  ";
                        sql1 += "  " + Variance_value_queue_161_5.ToArray()[4] + "," + Variance_value_queue_161_3.ToArray()[2] + "," + Slope1_Sum_queue_161_5 + ",  ";
                        sql1 += "  " + Slope1_Sum_queue_161_3 + "," + Slope2_value_Array_161_1 + "," + AVG_Value_Array_161_1.ToArray()[0] + ",Convert(varchar(100),GETDATE(),120)," + RC_Value_array_161_3[2] + "," + Axis_X_161 + "," + Axis_Y_161 + ")  ";


                        if (sql_db.OpenMyConnection1())
                        {
                            sql_db.ExecuteSQL1(sql1);
                        }


                        //兑铁开始时刻捕捉
                        if ((Variance_value_queue_161_10.ToArray()[9] > Variance_limit_low_begin) && (Variance_value_queue_161_10.ToArray()[9] < Variance_limit_high_begin) && (Crane_Weight_Curent_161 > weight_limit_begin) && (Slope1_Sum_queue_161_5 < Slope1_5_high_begin) && (Slope1_Sum_queue_161_5 > Slope1_5_lower_begin))
                        {
                            if ((AVG_Value_Array_161_1[0] > AVG_lower_begin) && (AVG_Value_Array_161_1[0] < AVG_high_begin))
                            {
                                if ((Slope1_value_Array_161_3[2] < Slope1_1_high_begin) && (Slope1_value_Array_161_3[2] > Slope1_1_lower_begin)) //一阶斜率
                                {
                                    ChargeData_Time_161 = 400;
                                    string sql11 = @"insert into dbo.crane_charging_data(crane_id,charging_start_weight,charging_start,crane_server_time,timestamp) values('161'," + Runtime_Value_array_161[0] + ",1,'" + datatime_str_current_161 + "',Convert(varchar(100),GETDATE(),120))";

                                    if (sql_db.OpenMyConnection1())
                                    {
                                        sql_db.ExecuteSQL1(sql11);
                                    }
                                }
                            }
                            // float Runtime_Value_array_temp = Runtime_Value_array[0];

                        }

                        //兑铁结束时刻捕捉
                        if (ChargeData_Time_161 > 50)
                        //{
                            if ((Variance_value_queue_161_10.ToArray()[9] > Variance_limit_low_end) && (Variance_value_queue_161_10.ToArray()[9] < Variance_limit_high_end) && (Crane_Weight_Curent_161 > weight_limit_end) && (Slope1_Sum_queue_161_3 < Slope1_3_high_end) && (Slope1_Sum_queue_161_3 > Slope1_3_lower_end))
                            {
                                if ((AVG_Value_Array_161_1[0] > AVG_lower_end) && (AVG_Value_Array_161_1[0] < AVG_high_end))
                                {
                                    //if ((Slope1_value_Array_161_3[2] < Slope1_1_high_end) && (Slope1_value_Array_161_3[2] > Slope1_1_lower_end)) //一阶斜率
                                    //{
                                    if ((RC_Value_array_161_3[2] - RC_Value_array_161_3[1] >= 0) && (RC_Value_array_161_3[1] - RC_Value_array_161_3[0]<=0))
                                    {
                                        string sql11 = @"insert into dbo.crane_charging_data(crane_id,charging_stop_weight,charging_stop,crane_server_time,timestamp) values('161'," + Runtime_Value_array_161[8] + ",1,'" + datatime_str_current_161 + "',Convert(varchar(100),GETDATE(),120))";

                                        if (sql_db.OpenMyConnection1())
                                        {
                                            sql_db.ExecuteSQL1(sql11);
                                        }
                                    }
                                   // }
                                }
                            }
                        //}


                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Crane_Id=161,Crane_Weight={0},Datatime_Str={1},X={2},Y={3}", Crane_Weight_Curent_161, datatime_str_current_161, Axis_X_161, Axis_Y_161);
                        count++;
                        if (count > 20)
                        {
                            Console.Clear();
                            count = 0;
                        }
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //162
                if ((Variance_value_queue_162_10.Count == 10) && (AVG_Value_Array_162_1.Count == 1) && (Variance_value_queue_162_5.Count > 4) && (RC_Value_array_162_3.Count == 3) && ((Axis_X_162 == 51440) || (Axis_X_162 == 42744)))
                {
                    // if (Math.Abs(AVG_Value_Array_162[4] - Crane_Weight_Curent_162) > 1)
                    {
                        //基础数据获取
                        string sql1 = @"insert into dbo.Crane_data_162_Analysis";
                        sql1 += "(crane_id,crane_weight,crane_server_time,Variance_value_10,Variance_value_5,Variance_value_3,Slope1_value_5,Slope1_value_3,Slope2_value_1,AVG_value_3,timestamp,RC_value_1,axis_x,axis_y)  ";
                        sql1 += "  values('162'," + Crane_Weight_Curent_162 + ",'" + datatime_str_current_162 + "'," + Variance_value_queue_162_10.ToArray()[9] + ",  ";
                        sql1 += "  " + Variance_value_queue_162_5.ToArray()[4] + "," + Variance_value_queue_162_3.ToArray()[2] + "," + Slope1_Sum_queue_162_5 + ",  ";
                        sql1 += "  " + Slope1_Sum_queue_162_3 + "," + Slope2_value_Array_162_1 + "," + AVG_Value_Array_162_1.ToArray()[0] + ",Convert(varchar(100),GETDATE(),120)," + RC_Value_array_162_3[2] + "," + Axis_X_162 + "," + Axis_Y_162 + ")  ";


                        if (sql_db.OpenMyConnection1())
                        {
                            sql_db.ExecuteSQL1(sql1);
                        }


                        //兑铁开始时刻捕捉
                        if ((Variance_value_queue_162_10.ToArray()[9] > Variance_limit_low_begin) && (Variance_value_queue_162_10.ToArray()[9] < Variance_limit_high_begin) && (Crane_Weight_Curent_162 > weight_limit_begin) && (Slope1_Sum_queue_162_5 < Slope1_5_high_begin) && (Slope1_Sum_queue_162_5 > Slope1_5_lower_begin))
                        {
                            if ((AVG_Value_Array_162_1[0]>AVG_lower_begin)&&(AVG_Value_Array_162_1[0]<AVG_high_begin))
                            {
                                if ((Slope1_value_Array_162_3[2] < Slope1_1_high_begin) && (Slope1_value_Array_162_3[2] > Slope1_1_lower_begin))//一阶斜率
                                {
                                    ChargeData_Time_162 = 400;
                                    string sql11 = @"insert into dbo.crane_charging_data(crane_id,charging_start_weight,charging_start,crane_server_time,timestamp) values('162'," + Runtime_Value_array_162[0] + ",1,'" + datatime_str_current_162 + "',Convert(varchar(100),GETDATE(),120))";

                                    if (sql_db.OpenMyConnection1())
                                    {
                                        sql_db.ExecuteSQL1(sql11);
                                    }
                                }
                                // float Runtime_Value_array_temp = Runtime_Value_array[0];
                            }
                        }


                        //兑铁结束时刻捕捉
                        //if (ChargeData_Time_162 > 50)
                        //{
                            if ((Variance_value_queue_162_10.ToArray()[9] > Variance_limit_low_end) && (Variance_value_queue_162_10.ToArray()[9] < Variance_limit_high_end) && (Crane_Weight_Curent_162 > weight_limit_end) && (Slope1_Sum_queue_162_3 < Slope1_3_high_end) && (Slope1_Sum_queue_162_3 > Slope1_3_lower_end))
                            {
                                if ((AVG_Value_Array_162_1[0] > AVG_lower_end) && (AVG_Value_Array_162_1[0] < AVG_high_end))
                                {
                                    //if ((Slope1_value_Array_162_3[2] < Slope1_1_high_end) && (Slope1_value_Array_162_3[2] > Slope1_1_lower_end)) //一阶斜率
                                    //{
                                    if ((RC_Value_array_162_3[2] - RC_Value_array_162_3[1] >= 0) && (RC_Value_array_162_3[1] - RC_Value_array_162_3[0]<=0))
                                    {
                                        string sql11 = @"insert into dbo.crane_charging_data(crane_id,charging_stop_weight,charging_stop,crane_server_time,timestamp) values('162'," + Runtime_Value_array_162[8] + ",1,'" + datatime_str_current_162 + "',Convert(varchar(100),GETDATE(),120))";

                                        if (sql_db.OpenMyConnection1())
                                        {
                                            sql_db.ExecuteSQL1(sql11);
                                        }
                                    }
                                    //}
                                }
                            }
                        //}



                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Crane_Id=162,Crane_Weight={0},Datatime_Str={1},X={2},Y={3}", Crane_Weight_Curent_162, datatime_str_current_162, Axis_X_162, Axis_Y_162);
                        count++;
                        if (count > 20)
                        {
                            Console.Clear();
                            count = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }

        }





        public void Data_Save_Process()
        {
            while (true)
            {
                //实时数据更新
                //string sql2 = @"update dbo.crane_runtime_data set crane_weight = case crane_id when '161' then " + Crane_Weight_Curent_161 + " when '162' then " + Crane_Weight_Curent_162 + " end ,crane_server_time = '" + datatime_str_current_161 + "' ,timestamp=Convert(varchar(100),GETDATE(),120) ";

                string sql2 = @"update dbo.crane_runtime_data set crane_weight = case crane_id when '161' then " + Crane_Weight_Curent_161 + " when '162' then " + Crane_Weight_Curent_162 + " end,axis_x = case crane_id when '161' then  " + Axis_X_161 + " when '162' then " + Axis_X_162 + " end,axis_y = case crane_id when '161' then  " + Axis_Y_161 + " when '162' then " + Axis_Y_162 + " end ,crane_server_time = '" + datatime_str_current_161 + "' ,timestamp=Convert(varchar(100),GETDATE(),120)";
                if (sql_db.OpenMyConnection1())
                {
                    sql_db.ExecuteSQL1(sql2);
                }

                Analysis_Data_Get();

                if (ChargeData_Time_161 >0)
                {
                    ChargeData_Time_161--;
                }

                if (ChargeData_Time_162 > 0)
                {
                    ChargeData_Time_162--;
                }

                Thread.Sleep(500);
            }
 
        }


    }

}
