﻿
namespace AtNet.Cms.Sql
{
    public class SqlServerSqlPack:SqlPack
    {
        internal SqlServerSqlPack()
        {
        }

        public override string Archive_GetAllArchive
        {
            get
            {
                return @"SELECT $PREFIX_archive.[ID],[strid],[alias],[cid],title,$PREFIX_archive.location,[flags],thumbnail,author,
                        [author],[viewcount],[lastmodifydate],[Tags],[Outline],[Content],[CreateDate] FROM $PREFIX_archive INNER JOIN $PREFIX_category ON
                        $PREFIX_category.[ID]=$PREFIX_archive.[cid] WHERE " +SqlConst.Archive_NotSystemAndHidden + "  ORDER BY [CreateDate] DESC";
            }
        }

        public override string Archive_GetSelfAndChildArchives
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],[strid],[alias],cid,title,$PREFIX_archive.location,[flags],[Outline],author,tags,source,
                        thumbnail,[Content],lastmodifydate,[CreateDate],$PREFIX_category.[Name],$PREFIX_category.[Tag]
                        FROM $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_category.[ID]=$PREFIX_archive.[cid]
                        WHERE " + SqlConst.Archive_NotSystemAndHidden + @" AND (lft>=@lft AND rgt<=@rgt) 
                         AND $PREFIX_category.siteId=@siteId 
                        ORDER BY [CreateDate] DESC";
            }
        }
        public override string Archive_GetSelfAndChildArchiveExtendValues
        {
            get
            {
                return @"
                        SELECT v.id as id,fieldId as extendFieldId,f.name as fieldName,fieldValue as extendFieldValue,relationId
	                    FROM $PREFIX_extendValue v INNER JOIN $PREFIX_extendField f ON v.fieldId=f.id
	                    WHERE relationType=@relationType AND relationId IN (

                        SELECT TOP {0} $PREFIX_archive.id
                        FROM $PREFIX_archive INNER JOIN $PREFIX_category ON 
                        $PREFIX_category.id=$PREFIX_archive.cid
                        WHERE " + SqlConst.Archive_NotSystemAndHidden
                                + @" AND (lft>=@lft AND rgt<=@rgt) 
                        ORDER BY createdate DESC
                        
                        )";
            }
        }

        public override string Archive_GetArchivesExtendValues
        {
            get
            {
                return @"
                        SELECT v.id as id,fieldId as extendFieldId,f.name as fieldName,fieldValue as extendFieldValue,relationId
	                    FROM $PREFIX_extendValue v INNER JOIN $PREFIX_extendField f ON v.fieldId=f.id
	                    WHERE relationType=@relationType AND relationId IN (

                        SELECT TOP {0} $PREFIX_archive.id FROM $PREFIX_archive
                        INNER JOIN $PREFIX_category ON $PREFIX_category.id=$PREFIX_archive.cid
                        WHERE tag=@Tag AND " + SqlConst.Archive_NotSystemAndHidden
                        + @" ORDER BY createdate DESC,$PREFIX_archive.id
                        
                        )";
            }
        }

        public override string Archive_GetArchivesByCategoryAlias
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.id,strid,[alias],cid,flags,title,$PREFIX_archive.location,
                        outline,thumbnail,author,lastmodifydate,source,tags,
                        [content],[viewcount],[createdate] FROM $PREFIX_archive INNER JOIN $PREFIX_category ON
                        $PREFIX_category.[ID]=$PREFIX_archive.cid WHERE siteid=@siteId AND  tag=@tag AND " +
                        SqlConst.Archive_NotSystemAndHidden + @" ORDER BY [createdate] DESC";
            }
        }

        public override string Archive_GetArchivesByModuleID
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],[strid],[alias],[flags],title,$PREFIX_archive.location,[Outline],
                        thumbnail,[Content],[ViewCount],[CreateDate]
                        $PREFIX_category.[Name],$PREFIX_category.[Tag],[viewcount],[createdate],[lastmodifydate]
                        FROM $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_category.ID=$PREFIX_archive.cid
                        AND $PREFIX_category.siteid=@siteid
                        WHERE " + SqlConst.Archive_NotSystemAndHidden + @" AND siteid=@siteId AND ModuleID=@ModuleID ORDER BY [CreateDate] DESC";
            }
        }
       
        public override string Archive_GetArchivesByViewCountDesc
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],$PREFIX_category.[ID] as cid,
                        flags,[strid],[alias],title,$PREFIX_archive.location,[Outline],[Content],thumbnail,
                        $PREFIX_category.[Name],$PREFIX_category.[Tag]
                        FROM $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_category.[ID]=$PREFIX_archive.[cid]
                        WHERE " + SqlConst.Archive_NotSystemAndHidden + @" AND siteid=@siteId AND  (lft>=@lft AND rgt<=@rgt)
                        ORDER BY [ViewCount] DESC";
            }
        }

         public override string Archive_GetArchivesByViewCountDesc_Tag
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],$PREFIX_category.[ID] as cid,flags,
                        [strid],[alias],title,$PREFIX_archive.location,[Outline],thumbnail,[Content]
                        ,$PREFIX_category.[Name],$PREFIX_category.[Tag]
                        FROM $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_category.[ID]=$PREFIX_archive.[cid]
                        WHERE " + SqlConst.Archive_NotSystemAndHidden + @" AND siteid=@siteId AND tag=@tag
                        ORDER BY [ViewCount] DESC";
            }
        }

        public override string Archive_GetArchivesByModuleIDAndViewCountDesc
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],[cid],flags,[strid],[alias],title,$PREFIX_archive.location,[Outline],thumbnail,[Content],
                    $PREFIX_category.[Name],$PREFIX_category.[Tag] FROM $PREFIX_archive
				    INNER JOIN $PREFIX_category ON $PREFIX_category.ID=$PREFIX_archive.cid
                    WHERE " + SqlConst.Archive_NotSystemAndHidden + @" AND siteid=@siteId AND ModuleID=@ModuleID ORDER BY [ViewCount] DESC";
            }
        }


        public override string Archive_GetSpecialArchivesByCategoryID
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],flags,[strid],[alias],[cid],[flags],title,
                        $PREFIX_archive.location,[content],[outline],thumbnail,[tags],[createdate],[lastmodifydate]
                        ,[viewcount],[source] FROM $PREFIX_archive INNER JOIN $PREFIX_category ON
                    $PREFIX_category.[ID]=$PREFIX_archive.[cid] WHERE (lft>=@lft AND rgt<=@rgt) AND siteid=@siteId AND "
                    + SqlConst.Archive_Special + @" ORDER BY [CreateDate] DESC";
            }
        } 
        public override string Archive_GetSpecialArchivesByCategoryTag
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],[strid],[alias],[cid],[flags],title,$PREFIX_archive.location,[content],[outline],thumbnail,[tags],[createdate],[lastmodifydate]
                        ,[viewcount],[source] FROM $PREFIX_archive INNER JOIN $PREFIX_category ON
                    $PREFIX_category.[ID]=$PREFIX_archive.[cid] WHERE $PREFIX_category.[tag]=@CategoryTag AND siteid=@siteId AND "
                    + SqlConst.Archive_Special + @" ORDER BY [CreateDate] DESC";
            }
        }
        
        public override string Archive_GetSpecialArchivesByModuleID
        {
            get
            {
                return @"SELECT TOP {0} $PREFIX_archive.[ID],[strid],[alias],[cid],[flags],title,$PREFIX_archive.location,[content],[outline],thumbnail,[tags],[createdate],[lastmodifydate]
                        ,[viewcount],[source] FROM $PREFIX_archive INNER JOIN $PREFIX_category ON
                    $PREFIX_category.[ID]=$PREFIX_archive.[cid] WHERE $PREFIX_category.[ModuleID]=@moduleID AND siteid=@siteId AND "
                    + SqlConst.Archive_Special + @" ORDER BY [CreateDate] DESC";
            }
        }

        public override string Archive_GetFirstSpecialArchiveByCategoryID
        {
            get
            {
                return "SELECT TOP 1 * FROM $PREFIX_archive WHERE [cid]=@CategoryId AND siteid=@siteId AND " 
                + SqlConst.Archive_Special + @" ORDER BY [CreateDate] DESC"; }
        }

        public override string Archive_GetPreviousSameCategoryArchive
        {
            get
            {
                return @"SELECT TOP 1 [ID],[strid],[alias],a.[cid],title,a.location,thumbnail,a.[createdate] FROM $PREFIX_archive a,
                                 (SELECT TOP 1 [cid],[CreateDate] FROM $PREFIX_archive WHERE ID=@id) as t
                                 WHERE a.[cid]=t.[cid] AND a.[CreateDate]<t.[CreateDate] AND " + SqlConst.Archive_NotSystemAndHidden +
                                 " ORDER BY a.[CreateDate] DESC ";
            }
        }

        public override string Archive_GetNextSameCategoryArchive
        {
            get
            {
                return @"SELECT TOP 1 [ID],[strid],[alias],a.[cid],title,a.location,thumbnail,a.[createdate] FROM $PREFIX_archive a,
                                 (SELECT TOP 1 [cid],[CreateDate] FROM $PREFIX_archive WHERE [ID]=@id) as t
                                 WHERE a.[cid]=t.[cid] AND a.[CreateDate]>t.[CreateDate] AND " + SqlConst.Archive_NotSystemAndHidden +
                                 " ORDER BY a.[CreateDate]";
            }
        }

        public override string Archive_GetPagedArchivesByCategoryID_pagerquery
        {
            get
            {
            	/*
                return @"SELECT TOP $[pagesize] $PREFIX_archive.[ID] AS ID,* FROM $PREFIX_archive 
                        INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.[ID]
                        WHERE $[condition] AND $PREFIX_archive.[id] NOT IN 
                        (SELECT TOP $[skipsize] $PREFIX_archive.ID  FROM $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.[ID]
                        WHERE $[condition] ORDER BY [CreateDate] DESC) ORDER BY [CreateDate] DESC";
                */
                return @"SELECT * FROM (SELECT $PREFIX_archive.*,
                        ROW_NUMBER()OVER(ORDER BY [CreateDate] DESC) as rowNum
                        FROM $PREFIX_archive 
                        INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.[ID]
                        WHERE $PREFIX_category.siteId=@siteId AND (lft>=@lft AND rgt<=@rgt) 
                         AND " + SqlConst.Archive_NotSystemAndHidden + @") _t 
						WHERE rowNum BETWEEN $[skipsize]+1 AND ($[skipsize]+$[pagesize])";
            }
        }

        public override string Archive_GetpagedArchivesCountSql
        {
            get
            {
                return @"SELECT COUNT(a.id) FROM $PREFIX_archive a
                        Left JOIN $PREFIX_category c ON
                        a.cid=c.id Where {0}";
            }
        }


        public override string Archive_GetPagedArchivesByCategoryId
        {
            get
            {
                /*return @"SELECT TOP $[pagesize] a.[ID] AS [ID],[strid],[alias],title,$PREFIX_archive.location,thumbnail,
                        c.[Name] as [CategoryName],[cid],[flags],[Author],[Content],[Source],
                        [CreateDate],[ViewCount] FROM $PREFIX_archive a LEFT JOIN $PREFIX_category c
                        ON a.cid=c.ID INNER JOIN $PREFIX_modules m ON c.[moduleid]=m.[id]
                        WHERE $[condition] AND a.[ID] NOT IN 
                        (SELECT TOP $[skipsize] a1.[ID] FROM $PREFIX_archive a1
                         LEFT JOIN $PREFIX_category c1 ON a1.cid=c1.ID
                        INNER JOIN $PREFIX_modules ON c1.[moduleid]=m1.[id]
                        WHERE $[condition] ORDER BY [$[orderByField]] $[orderASC]) ORDER BY [$[orderByField]] $[orderASC]";*/
             
                return @"SELECT * FROM (SELECT a.id AS id,strid,alias,title,
                        a.location,thumbnail,c.name as categoryName,[cid],[flags],[author],[content],
                        [source],[createDate],[viewCount],
						ROW_NUMBER()OVER(ORDER BY [$[orderByField]] $[orderASC]) as rowNum
						FROM $PREFIX_archive a LEFT JOIN $PREFIX_category c
                        ON a.cid=c.ID WHERE $[condition]) _t
						WHERE rowNum BETWEEN $[skipsize]+1 AND ($[skipsize]+$[pagesize])";
            }
        }


        public override string Archive_GetPagedOperations
        {
            get { 
        		//return "SELECT TOP $[pagesize] * FROM $PREFIX_operation WHERE ID NOT IN (SELECT TOP $[skipsize] ID FROM $PREFIX_operation)"; 
        		return @"SELECT * FROM (SELECT *,
        			ROW_NUMBER()OVER(ORDER BY id) as rowNum
			 		FROM $PREFIX_operation) _t WHERE rowNum BETWEEN $[skipsize]+1 AND ($[skipsize]+$[pagesize])";
        	}
        }

        public override string Message_GetPagedMessages
        {
            get { return @"SELECT * FROM (SELECT *,
        			ROW_NUMBER()OVER(ORDER BY [SendDate] DESC) as rowNum FROM $PREFIX_Message
				    WHERE Recycle=0 AND $[condition] ORDER BY [SendDate] DESC) _t
					WHERE rowNum BETWEEN $skipsize+1 AND ($[skipsize]+$[pagesize])"; }
        }
        public override string Member_GetPagedMembers
        {
            get
            {
                //return @"SELECT TOP $[pagesize] [id],[username],[avatar],[nickname],[RegIp],[RegTime],[LastLoginTime] FROM $PREFIX_member INNER JOIN $PREFIX_memberdetails
                //        ON $PREFIX_member.[ID]=$PREFIX_memberdetails.[UID] WHERE [ID] NOT IN (SELECT TOP $[skipsize] [ID] FROM $PREFIX_member ORDER BY [ID] DESC) ORDER BY [ID] DESC";
                return @"SELECT * FROM (SELECT $PREFIX_member.[id],[username],[avatar],[nickname],[RegIp],[RegTime],[LastLoginTime],
						 ROW_NUMBER()OVER(ORDER BY $PREFIX_member.id) as rowNum
 						 FROM $PREFIX_member INNER JOIN $PREFIX_memberdetails
                        ON $PREFIX_member.[ID]=$PREFIX_memberdetails.[UID] ORDER BY $PREFIX_member.[ID] DESC) _t
						WHERE rowNum BETWEEN $skipsize+1 AND ($[skipsize]+$[pagesize])";
            }
        }

        public override string Archive_GetPagedSearchArchives
        {
            get
            {
                // $PREFIX_category.name as cname,
                //        $PREFIX_category.tag,

                return @"SELECT * FROM (SELECT $PREFIX_archive.*,
                     ROW_NUMBER() OVER($[orderby]) as rowNum
					 FROM $PREFIX_archive INNER JOIN $PREFIX_category
                    ON $PREFIX_archive.cid=$PREFIX_category.id
                    WHERE $[condition] AND $PREFIX_category.siteid=$[siteid]
                    AND ([Title] LIKE '%$[keyword]%' OR [Outline] LIKE '%$[keyword]%'
				   OR [Content] LIKE '%$[keyword]%' OR [Tags] LIKE '$[keyword]%')) _t
                    WHERE rowNum BETWEEN $[skipsize]+1 AND
                    $[skipsize]+$[pagesize] ORDER BY rowNum";
			}
        }

        public override string Archive_GetPagedSearchArchivesByModuleID
        {
            get
            {
            	/*
                return @"SELECT TOP $[pagesize] $PREFIX_archive.[ID] AS ID,* FROM  $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.[ID]
                    WHERE $[condition] AND $PREFIX_category.[ModuleID]=$[moduleid] AND ([Title] LIKE '%$[keyword]%' OR [Outline] LIKE '%$[keyword]%' OR [Content] LIKE '%$[keyword]%' OR [Tags] LIKE '%$[keyword]%') AND
                    $PREFIX_archive.[ID] NOT IN (SELECT TOP $[skipsize] $PREFIX_archive.[ID] FROM $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.[ID]
                   WHERE $[condition] AND $PREFIX_category.[ModuleID]=$[moduleid] AND ([Title] LIKE '%$[keyword]%' OR [Outline] LIKE '%$[keyword]%' OR [Content] LIKE '%$[keyword]%' OR [Tags] LIKE '%$[keyword]%') $[orderby]) $[orderby]";
            	*/
                return @"SELECT * FROM (SELECT $PREFIX_archive.*,
                     ROW_NUMBER()OVER($[orderby]) as rowNum
					 FROM $PREFIX_archive INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.id
                    WHERE $[condition] AND $PREFIX_category.[ModuleID]=$[moduleid] AND ([Title] LIKE '%$[keyword]%' OR [Outline] LIKE '%$[keyword]%' OR [Content] LIKE '%$[keyword]%' OR [Tags] LIKE '%$[keyword]%') $[orderby]) _t
					WHERE rowNum BETWEEN $[skipsize]+1 AND ($[skipsize]+$[pagesize])";
			}
        }

        public override string Archive_GetPagedSearchArchivesByCategoryID
        {
            get
            {
                return @"SELECT * FROM (SELECT $PREFIX_archive.*,
                         ROW_NUMBER()OVER($[orderby]) as rowNum
					     FROM  $PREFIX_archive INNER JOIN $PREFIX_category 
                         ON $PREFIX_archive.cid=$PREFIX_category.id
                        WHERE $[condition] AND $PREFIX_category.siteid=@siteId AND
                        ($PREFIX_category.lft>=@lft AND $PREFIX_category.rgt<=@rgt) 
                        AND (title LIKE '%$[keyword]%' OR Outline LIKE '%$[keyword]%'
                        OR [Content] LIKE '%$[keyword]%' OR [Tags] LIKE '%$[keyword]%')
					) _t WHERE rowNum BETWEEN $[skipsize]+1 AND ($[skipsize]+$[pagesize]) ORDER BY rowNum";
            }
        }

        public override string Archive_GetPagedOperationsByAvialble
        {
            get { return "SELECT * FROM (SELECT *,ROW_NUMBER()OVER(ORDER BY id) as rowNum FROM $PREFIX_operation WHERE $[condition]) _t  WHERE rowNum BETWEEN $skipsize AND ($skipsize+$pagesize)"; }
        }

        public override string Archive_GetArchivesByCondition
        {
            get
            {
                return @"SELECT $PREFIX_archive.[ID],[strid],[alias],[cid],title,$PREFIX_archive.location,[Tags],[Outline],thumbnail,[Content],[IsSystem],[IsSpecial],[Visible],[CreateDate] FROM $PREFIX_archive INNER JOIN $PREFIX_category ON
                    $PREFIX_category.[ID]=$PREFIX_archive.[cid] WHERE {0} ORDER BY [CreateDate] DESC";
            }
        }

        public override string Comment_GetCommentsForArchive
        {
            get { return "SELECT * FROM $PREFIX_comment LEFT JOIN $PREFIX_member ON [MemberID]=$PREFIX_member.[ID] WHERE [archiveID]=@archiveId"; }
        }


        public override string Link_AddSiteLink
        {

            get { return @"INSERT INTO $PREFIX_link (siteid,pid,[type],[text],[uri],
                    imgurl,[target],bind, visible,orderIndex)VALUES(@siteid,@pid,@TypeID,
                    @Text,@Uri,@imgurl,@Target,@bind,@visible,@orderIndex)";
            }
        }

        public override string Link_UpdateSiteLink
        {
            get { return @"UPDATE $PREFIX_link SET pid=@pid,[type]=@TypeID,[text]=@Text,
                            visible=@visible,[uri]=@Uri,imgurl=@imgurl,[target]=@Target,
                            bind=@bind,orderIndex=@orderIndex WHERE [ID]=@LinkId AND siteid=@siteId";
            }
        }

        public override string Archive_Add
        {
            get
            {
                return @"INSERT INTO $PREFIX_archive(strId,alias,cid,author,title,[flags],location,
                                    [Source],thumbnail,[Outline],[Content],[Tags],[Agree],[Disagree],[ViewCount],
                                    [CreateDate],[LastModifyDate])
                                    VALUES(@strId,@alias,@CategoryId,@Author,@Title,@Flags,@location,
                                    @Source,@thumbnail ,@Outline,@Content,@Tags,0,0,0,@CreateDate,
                                    @LastModifyDate)";
            }
        }

        public override string Comment_GetCommentDetailsListForArchive
        {
            get
            {
                return @"SELECT $PREFIX_comment.ID as cid,[IP],[content],[createDate],
                       $PREFIX_member.ID as uid,[Avatar],[NickName] FROM $PREFIX_comment INNER JOIN $PREFIX_member ON
                       $PREFIX_comment.[memberID]=$PREFIX_member.[ID] WHERE [archiveID]=@archiveID ORDER BY [createDate] DESC";
            }
        }

        public override string Archive_Update
        {
            get
            {
                return @"UPDATE $PREFIX_archive SET [cid]=@CategoryId,[Title]=@Title,flags=@flags,
                                    [Alias]=@Alias,location=@location,[Source]=@Source,lastmodifydate=@lastmodifyDate,
                                    thumbnail=@thumbnail,[Outline]=@Outline,[Content]=@Content,[Tags]=@Tags WHERE id=@id";
            }
        }

        public override string Archive_GetSearchRecordCountByModuleID
        {
            get
            {
                return @"SELECT COUNT(0) FROM $PREFIX_archive
                        INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.[ID]
                        WHERE {2} AND $PREFIX_category.moduleid={0} AND ([Title] LIKE '%{1}%'
                        OR [Outline] LIKE '%{1}%' OR [Content] LIKE '%{1}%' OR [Tags] LIKE '%{1}%')";
            }
        }

        public override string Archive_GetSearchRecordCountByCategoryID
        {
            get
            {
                return @"SELECT COUNT($PREFIX_archive.id) FROM $PREFIX_archive
                         INNER JOIN $PREFIX_category ON $PREFIX_archive.[cid]=$PREFIX_category.[ID]
                         WHERE {1} AND $PREFIX_category.siteid=@siteId 
                            AND ($PREFIX_category.lft>=@lft AND $PREFIX_category.rgt<=@rgt)
                         AND ([Title] LIKE '%{0}%' OR [Outline] LIKE '%{0}%' OR [Content] LIKE '%{0}%' OR [Tags] LIKE '%{0}%')";
            }
        }

        public override string Comment_AddComment
        {
            get { return "INSERT INTO $PREFIX_comment ([ArchiveID],[MemberID],[IP],[Content],[Recycle],[CreateDate])VALUES(@ArchiveId,@MemberID,@IP,@Content,@Recycle,@CreateDate)"; }
        }

        public override string Member_RegisterMember
        {
            get { return "INSERT INTO $PREFIX_member([Username],[Password],[Avatar],[Sex],[Nickname],[Note],[Email],[Telephone])values(@username,@password,@Avatar,@sex,@nickname,@note,@email,@Telephone)"; }
        }

        public override string Member_Update
        {
            get { return "UPDATE $PREFIX_member SET [Password]=@Password,[Avatar]=@Avatar,[Sex]=@Sex,[Nickname]=@Nickname,[Email]=@Email,[Telephone]=@Telephone,[Note]=@Note WHERE [ID]=@id"; }
        }

        public override string Table_GetLastedRowID
        {
            get { return "SELECT TOP 1 id FROM $PREFIX_table_row ORDER BY id DESC"; }
        }
        public override string Table_InsertRowData
        {
            get { return "INSERT INTO $PREFIX_table_rowdata (rid,cid,[value]) VALUES(@rowid,@columnid,@value)"; }
        }

        public override string Table_GetPagedRows
        {
            get
            {
                return @"SELECT * FROM (SELECT *,
                        ROW_NUMBER()OVER(ORDER BY submittime DESC) as rowNum
						FROM $PREFIX_table_row WHERE tableid=$[tableid]
                        ORDER BY submittime DESC) _t
						WHERE rowNum BETWEEN $skipsize+1 AND ($[skipsize]+$[pagesize])";
            }
        }
    }
}
