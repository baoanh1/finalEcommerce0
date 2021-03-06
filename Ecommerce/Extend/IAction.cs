/*
 * Copyright (c) 2019 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

namespace Ecommerce.Extend
{
    public interface IAction
    {
        /// <summary>
        /// Gets/sets the internal id of the action.
        /// </summary>
        string InternalId { get; set; }
    }
}