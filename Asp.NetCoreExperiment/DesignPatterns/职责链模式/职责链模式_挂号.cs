using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.职责链模式
{
    #region 实体类
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        男,
        女,
        其他
    }
    /// <summary>
    /// 病人信息
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public uint Age
        { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex
        { get; set; }

    }

    /// <summary>
    /// 挂号实体类
    /// </summary>
    public class ClinicRegisterEntity
    {
        /// <summary>
        /// 病人信息
        /// </summary>
        public Patient Patient
        { get; set; }

        /// <summary>
        /// 是否医保病人
        /// </summary>
        public bool IsMedical
        { get; set; }
        /// <summary>
        /// 是否往健康平台上报数据
        /// </summary>
        public bool IsHealthPlatform
        { get; set; }
        /// <summary>
        /// 是否简易挂号
        /// </summary>
        public bool IsSimpleRegisterClinic
        { get; set; }
    }

    #endregion

    #region 挂号抽象类
    public abstract class ClinicRegister
    {
        protected ClinicRegister _clinicRegister;
        public void Next(ClinicRegister clinicRegister)
        {
            _clinicRegister = clinicRegister;
        }
        /// <summary>
        /// 登记
        /// </summary>
        /// <returns></returns>
        public abstract bool Register(ClinicRegisterEntity clinicRegisterEntity);

    }
    #endregion

    #region 处理医保登记事宜
    /// <summary>
    /// 处理医保登记事宜
    /// </summary>
    public class MedicalRegister : ClinicRegister
    {
        public override bool Register(ClinicRegisterEntity clinicRegisterEntity)
        {
            if (clinicRegisterEntity.IsMedical)
            {
                Console.WriteLine("处理医保挂号登记事宜！");
            }
            return _clinicRegister.Register(clinicRegisterEntity);
        }
    }
    #endregion

    #region 处理健康平台事宜
    /// <summary>
    /// 处理健康平台事宜
    /// </summary>
    public class HealthPlatformRegister : ClinicRegister
    {
        public override bool Register(ClinicRegisterEntity clinicRegisterEntity)
        {
            if (clinicRegisterEntity.IsHealthPlatform)
            {
                Console.WriteLine("处理健康平台事宜！");
            }
            return _clinicRegister.Register(clinicRegisterEntity);
        }
    }
    #endregion

    #region 处理简易挂号和普通挂号事宜
    /// <summary>
    /// 处理简易挂号事宜
    /// </summary>
    public class SimpleClinicRegister : ClinicRegister
    {
        public override bool Register(ClinicRegisterEntity clinicRegisterEntity)
        {
            if (clinicRegisterEntity.IsSimpleRegisterClinic)
            {

                return SimpleRegister(clinicRegisterEntity);
            }
            else
            {
                return CommonRegister(clinicRegisterEntity);
            }
        }
        /// <summary>
        /// 简易挂号
        /// </summary>
        /// <param name="clinicRegisterEntity">挂号实体类</param>
        /// <returns></returns>
        bool SimpleRegister(ClinicRegisterEntity clinicRegisterEntity)
        {
            Console.WriteLine("简易挂号！");
            return true;
        }
        /// <summary>
        /// 普通挂号
        /// </summary>
        /// <param name="clinicRegisterEntity">挂号实体类</param>
        /// <returns></returns>
        bool CommonRegister(ClinicRegisterEntity clinicRegisterEntity)
        {
            Console.WriteLine("普通挂号！");
            return true;
        }
    }
    #endregion

    public class 职责链模式_挂号
    {
        public static void Start()
        {
            var clinicRegisterEntity = GetClinicRegister();
            Register(clinicRegisterEntity);
        }
        static ClinicRegisterEntity GetClinicRegister()
        {
            var clinicRegisterEntity = new ClinicRegisterEntity()
            {
                IsMedical = true,
                IsHealthPlatform = true,
                IsSimpleRegisterClinic = true,
                Patient = new Patient() { Name = "张三", Age = 20, Sex = Sex.女 }
            };
            return clinicRegisterEntity;
        }
        static void Register(ClinicRegisterEntity clinicRegisterEntity)
        {
            //医保登记
            var clinicRegister = new MedicalRegister();
            //健康平台登记
            var healthPlatformRegister = new HealthPlatformRegister();
            //本地his挂号，要么简易挂号，要么普通挂号
            var simpleRegister = new SimpleClinicRegister();

            //设置顺序，1、医保，2、健康平台 3、（简易或普通）挂号
            clinicRegister.Next(healthPlatformRegister);
            healthPlatformRegister.Next(simpleRegister);

            clinicRegister.Register(clinicRegisterEntity);
        }
       
    }


}
