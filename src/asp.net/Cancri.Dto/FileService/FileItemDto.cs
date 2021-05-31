// <copyright file="FileItemDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Dto.FileService
{
    using System;

    /// <summary>
    /// 文件 项.
    /// </summary>
    public class FileItemDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileItemDto"/> class.
        /// </summary>
        public FileItemDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileItemDto"/> class.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="isFolder">is folder.</param>
        /// <param name="sizeInByte">size in byte.</param>
        /// <param name="createTime">创建时间.</param>
        /// <param name="lastRevisionTime">最后写入时间.</param>
        public FileItemDto(string name, bool isFolder, long sizeInByte, DateTime createTime, DateTime? lastRevisionTime = null)
        {
            this.Name = name;
            this.IsFolder = isFolder;
            if (sizeInByte < 0)
            {
                this.Size = "--";
            }
            else if (sizeInByte < 1024)
            {
                this.Size = sizeInByte + "byte";
            }
            else if (sizeInByte < 1024 * 1024)
            {
                this.Size = sizeInByte + "KB";
            }
            else if (sizeInByte < 1024 * 1024 * 1024)
            {
                this.Size = sizeInByte + "MB";
            }
            else
            {
                this.Size = sizeInByte + "GB";
            }

            this.CreateTime = createTime;
            this.LastWriteTime = lastRevisionTime;
        }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is folder.
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// Gets or sets size.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets 创建时间.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets 最后写入时间.
        /// </summary>
        public DateTime? LastWriteTime { get; set; }
    }
}
