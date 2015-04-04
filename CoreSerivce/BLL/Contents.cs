using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Contents
    {
        public static BO.Contents Insert(BO.Contents ContentObj)
        {
            return DAL.Contents.Insert(ContentObj);
        }
        public static List<BO.Contents> SelectByState(int StateId, string Published, string Owner)
        {
            return DAL.Contents.SelectByState(StateId, Published,Owner);
        }
        public static BO.Contents SelectById(int Id)
        {
            return DAL.Contents.SelectById(Id);
        }
        public static bool UpdateOwner(int Id, int UserId)
        {
            return DAL.Contents.UpdateOwner(Id, UserId);
        }
        public static bool UpdateState(int Id, int StateId)
        {
            return DAL.Contents.UpdateState(Id, StateId);
        }
        public static List<BO.Contents> SelectByStatePublishedCategory(string Published, string Category, string Order)
        {
            return DAL.Contents.SelectByStatePublishedCategory(Published, Category, Order);
        }
        public static List<BO.Contents> Search(string SearchKey)
        {
            return DAL.Contents.SelectSearch(SearchKey);
        }
        public static void Update(BO.Contents ContentObj)
        {
            DAL.Contents.Update(ContentObj);
        }
        public static List<BO.Contents> frontendSelect(string Category, string count, string ordering)
        {
            List<BO.Contents> cntns = new List<BO.Contents>();

            if (Category== "-1")
            {
                cntns = DAL.Contents.frontendSelectLatest(count);
            }
            else
            {
                cntns = DAL.Contents.frontendSelect(Category, count,ordering);               
            }
            return cntns;
        }
        public static List<BO.Contents> frontendSelectByTagId(string tagId, string count)
        {
            return DAL.Contents.frontendSelectByTagId(tagId, count);
        }
        public static List<BO.Contents> frontendSelectRelatedByTag(string itemId, string count)
        {
            return DAL.Contents.frontendSelectRelatedByTag(itemId, count);
        }
        public static bool UpdateViewCount(int Id)
        {
            return DAL.Contents.UpdateViewCount(Id);
        }
        public static List<BO.Contents> frontendSelectMostViewed(string count, string hours)
        {
            return DAL.Contents.frontendSelectMostViewed(count, hours);
        }
        public static List<BO.Contents> frontendSearch(string Category, string count, string ordering, string SearchKey)
        {
            return DAL.Contents.frontendSearch(Category, count, ordering, SearchKey);
        }
       
    }
}
