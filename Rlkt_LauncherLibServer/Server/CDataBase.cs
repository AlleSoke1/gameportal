using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.Server
{

    public class BackLog
    {
        SqlCommand backCommand;
        dynamic callbackClass;
        public BackLog(SqlCommand cmd, dynamic callClass) { backCommand = cmd; callbackClass = callClass; }

        internal SqlCommand getBackLogCommand()
        {
            return backCommand;
        }
        internal dynamic getCallBack()
        {
            return callbackClass;
        }
    }

    public class Worker
    {
        private bool _busy = false;
        private SqlConnection _con;
        private string _dbname = "launcherapp";
        public int _workerId { get; set; }

        public Worker()
        {
            this.Connect();
        }

        private void Connect()
        {
            if (_con != null && _con.State == ConnectionState.Open) return;

            _con = new SqlConnection(String.Format(@"Server=WIN-RG7I9KEINKE\IRISSQL;DataBase={0};Integrated Security=SSPI", _dbname));
            _con.Open();

            if (_con.State == ConnectionState.Open)
            {
                Console.WriteLine("Database Success Connect!");
            }
            else
            {
                Console.WriteLine("Database Success Fail!");
                new ApplicationException("App cannot work without databas connotion.");
            }
        }

        public bool isBusy() { return _busy; }

        public bool Execute(SqlCommand cmd, dynamic callbackClass)
        {
            _busy = true;

            cmd.Connection = _con;

            SqlDataReader rdr = cmd.ExecuteReader();

            if(callbackClass != null)
                callbackClass.OnSqlResult(rdr, cmd);

            Console.WriteLine("Executed on worker "+_workerId);

            _busy = false;

            return true;
        }


    }

    public class WorkerFactory
    {
        private bool _initialized = false;
        private int _workers = 8; // 8 connections to database
        private int _lastUsedWorker = 0;
        public static List<Worker> workers = new List<Worker>();
        public static List<BackLog> backlog = new List<BackLog>();

        public WorkerFactory()
        {
            if (_initialized) return;

            for (int i = 0; i < _workers; i++)
            {
                Console.WriteLine("Initialize worker" + i);

                Worker worker = new Worker();
                worker._workerId = i;

                workers.Add(worker);
            }
            _initialized = true;

            new Thread(ProcessBacklog).Start();
        }

        private void ProcessBacklog()
        {
            while (true) {

                if(backlog.Count > 0)
                {
                    foreach(BackLog bl in backlog)
                    {
                        this.ExecuteSP(bl.getBackLogCommand(), bl.getCallBack());
                    }
                }

                Thread.Sleep(20);
            }
        }

        public void ExecuteSP(SqlCommand cmd, dynamic callbackClass)
        {
            bool _execStatus = false; //execute status!

            if (_lastUsedWorker > _workers) _lastUsedWorker = 0;
           
            while(true)
            {
                int workerJob = (_lastUsedWorker++ % _workers);
                if(!workers[workerJob].isBusy())
                {
                    workers[workerJob].Execute(cmd, callbackClass);
                    _execStatus = true;
                    break;
                }
            }

            //foreach(Worker w in workers)
            //{
            //    if(!w.isBusy())
            //    {
            //        w.Execute(cmd, callbackClass);
            //        _execStatus = true;
            //        break; //stop the foreach.
            //    }
            //}

            //All workers busy add command to backlog!
            if (!_execStatus)
                backlog.Add(new BackLog(cmd, callbackClass));
        }
        
    }

    public class CDatabase
    {
        WorkerFactory worker;
        
        public CDatabase()
        {
            try
            {
                worker = new WorkerFactory();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Execute(SqlCommand sql, dynamic callbackClass = null) //yep baby 
        {
            if(worker != null)
                worker.ExecuteSP(sql, callbackClass);
        }
    }
}
