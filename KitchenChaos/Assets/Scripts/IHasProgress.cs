using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress 
{
    /*
     * truoc day ta de event nay trong class CuttingCounter 
     * Dung de thay doi tien trinh cua viec cat thuc an (ca chua,bap cai)
     * Nhung hien tai thi co them StoveCounter(cai lo nuong) cung co thanh ProgressBar
     * Nen thay vi khai bao them Event thay doi tien trinh nua trong StoveCounter ta tao 1 interface khai bao event do
     * 
     */
    public event EventHandler<OnProgressChangedEventAgrs> OnProgressChanged;
    public class OnProgressChangedEventAgrs : EventArgs
    {
        public float progressNormalized;
    }
}
