﻿using System;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Fastlane
{
    /// <inheritdoc />
    /// <summary>
    /// Provides functionality for fastlane tools.
    /// </summary>
    public sealed class FastlaneProvider : IFastlaneProvider
    {
        private readonly ICakeContext _context;
        private IFastlaneDeliverProvider _fastlaneDeliverProvider;
        private IFastlaneMatchProvider _fastlaneMatchProvider;
        private IFastlanePemProvider _fastlanePemProvider;
        private IFastlanePilotProvider _fastlanePilotProvider;
        private IFastlaneSupplyProvider _fastlaneSupplyProvider;
        private IFastlaneToolProvider _fastlaneToolProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FastlaneProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FastlaneProvider(ICakeContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <inheritdoc />
        /// <summary>
        /// Delivers the specified deliver configuration.
        /// </summary>
        /// <example>
        ///     <code>
        ///         var configuration = new FastlaneDeliverConfiguration
        ///         {
        ///             CertificateType = CertificateType.Development,
        ///             AppIdentifier = "com.fastlane.cake",
        ///             ForceForNewDevices = true
        ///         };
        ///
        ///         Fastlane.Deliver(configuration);
        ///     </code>
        /// </example>
        /// <param name="deliverConfiguration">The fastlane deliver configuration.</param>
        [CakeAliasCategory("Deliver")]
        public void Deliver(FastlaneDeliverConfiguration deliverConfiguration = null)
        {
            if (_fastlaneDeliverProvider == null)
            {
                _fastlaneDeliverProvider = new FastlaneDeliverProvider(_context.FileSystem,
                    _context.Environment,
                    _context.ProcessRunner,
                    _context.Tools);
            }

            _fastlaneDeliverProvider.Deliver(deliverConfiguration ?? new FastlaneDeliverConfiguration());
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane deliver with the specified configuration action.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Fastlane.Deliver(config =>
        ///         {
        ///             config.CertificateType = CertificateType.Development;
        ///             config.AppIdentifier = "com.fastlane.cake";
        ///             config.ForceForNewDevices = true;
        ///         });
        ///     </code>
        /// </example>
        /// <param name="action">The action to build fastlane deliver configuration.</param>
        [CakeAliasCategory("Deliver")]
        public void Deliver(Action<FastlaneDeliverConfiguration> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var configuration = new FastlaneDeliverConfiguration();

            action(configuration);

            Deliver(configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane match with the specified configuration.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          var configuration = new FastlaneMatchConfiguration
        ///          {
        ///              CertificateType = CertificateType.Development,
        ///              AppIdentifier = "com.fastlane.cake",
        ///              ForceForNewDevices = true
        ///          };
        ///
        ///          Fastlane.Match(configuration);
        ///      </code>
        ///  </example>
        /// <param name="matchConfiguration">The fastlane match configuration.</param>
        [CakeAliasCategory("Match")]
        public void Match(FastlaneMatchConfiguration matchConfiguration = null)
        {
            if (_fastlaneMatchProvider == null)
            {
                _fastlaneMatchProvider = new FastlaneMatchProvider(_context.FileSystem,
                    _context.Environment,
                    _context.ProcessRunner,
                    _context.Tools);
            }

            _fastlaneMatchProvider.Match(matchConfiguration ?? new FastlaneMatchConfiguration());
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane match with the specified configuration action.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Fastlane.Match(config =>
        ///         {
        ///             config.CertificateType = CertificateType.Development;
        ///             config.AppIdentifier = "com.fastlane.cake";
        ///             config.ForceForNewDevices = true;
        ///         });
        ///     </code>
        /// </example>
        /// <param name="configurator">The fastlane match configuration action.</param>
        [CakeAliasCategory("Match")]
        public void Match(Action<FastlaneMatchConfiguration> configurator)
        {
            if (configurator == null)
            {
                throw new ArgumentNullException(nameof(configurator));
            }

            var configuration = new FastlaneMatchConfiguration();

            configurator(configuration);

            Match(configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pem with the specified configuration.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          var configuration = new FastlanePemConfiguration
        ///          {
        ///              Development = true,
        ///              ActiveDaysLimit = 45
        ///          };
        ///
        ///          Fastlane.Pem(configuration);
        ///      </code>
        ///  </example>
        /// <param name="pemConfiguration">The fastlane pem configuration.</param>
        [CakeAliasCategory("Pem")]
        public void Pem(FastlanePemConfiguration pemConfiguration)
        {
            if (_fastlanePemProvider == null)
            {
                _fastlanePemProvider = new FastlanePemProvider(_context.FileSystem,
                _context.Environment,
                _context.ProcessRunner,
                _context.Tools);
            }

            _fastlanePemProvider.Pem(pemConfiguration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pem with the specified configuration action.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          Fastlane.Pem(config =>
        ///             {
        ///                 config.Development = true;
        ///                 config.ActiveDaysLimit = 45;
        ///             });
        ///      </code>
        ///  </example>
        /// <param name="configurator">The fastlane pem configuration action.</param>
        [CakeAliasCategory("Pem")]
        public void Pem(Action<FastlanePemConfiguration> configurator)
        {
            if (configurator == null)
            {
                throw new ArgumentNullException(nameof(configurator));
            }

            var configuration = new FastlanePemConfiguration();

            configurator(configuration);

            Pem(configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pilot with the specified configuration.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          var configuration = new FastlanePilotConfiguration
        ///          {
        ///             AppIdentifier = "com.fastlane.cake",
        ///             Distribute = true,
        ///             WaitProcessingInterval = 45
        ///          };
        ///
        ///          Fastlane.Pilot(configuration);
        ///      </code>
        ///  </example>
        /// <param name="pilotConfiguration">The fastlane pilot configuration.</param>
        [CakeAliasCategory("Pilot")]
        public void Pilot(FastlanePilotConfiguration pilotConfiguration = null)
        {
            if (_fastlanePilotProvider == null)
            {
                _fastlanePilotProvider = new FastlanePilotProvider(_context.FileSystem,
                    _context.Environment,
                    _context.ProcessRunner,
                    _context.Tools);
            }

            _fastlanePilotProvider.Pilot(pilotConfiguration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pilot with the specified configuration action.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          Fastlane.Pilot(config =>
        ///             {
        ///                 config.AppIdentifier = "com.fastlane.cake";
        ///                 config.Distribute = true;
        ///                 config.WaitProcessingInterval = 45;
        ///             });
        ///      </code>
        ///  </example>
        /// <param name="configurator">The fastlane pilot configuration action.</param>
        [CakeAliasCategory("Pilot")]
        public void Pilot(Action<FastlanePilotConfiguration> configurator)
        {
            if (configurator == null)
            {
                throw new ArgumentNullException(nameof(configurator));
            }

            var configuration = new FastlanePilotConfiguration();

            configurator(configuration);

            Pilot(configuration);
        }

        /// <inheritdoc />
        ///  <summary>
        ///  Executes fastlane supply with the specified configuration.
        ///  </summary>
        ///   <example>
        ///       <code>
        ///           var configuration = new FastlaneSupplyConfiguration
        ///           {
        ///                 ApkFilePath = "./artifacts/android/cake.fastlane.apk",
        ///                 SkipUploadMetadata = true,
        ///                 SkipUploadImages = true,
        ///                 SkipUploadScreenShots = true
        ///           };
        ///
        ///           Fastlane.Supply(configuration);
        ///       </code>
        ///   </example>
        ///  <param name="supplyConfiguration">The fastlane supply configuration.</param>
        public void Supply(FastlaneSupplyConfiguration supplyConfiguration = null)
        {
            if (_fastlaneSupplyProvider == null)
            {
                _fastlaneSupplyProvider = new FastlaneSupplyProvider(_context.FileSystem,
                    _context.Environment,
                    _context.ProcessRunner,
                    _context.Tools);
            }

            _fastlaneSupplyProvider.Supply(supplyConfiguration ?? new FastlaneSupplyConfiguration());
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane supply with the specified configuration action.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          Fastlane.Supply(config =>
        ///             {
        ///                 config.ApkFilePath = "./artifacts/android/cake.fastlane.apk";
        ///                 config.SkipUploadMetadata = true;
        ///                 config.SkipUploadImages = true;
        ///                 config.SkipUploadScreenShots = true;
        ///             });
        ///      </code>
        ///  </example>
        /// <param name="configurator">The fastlane supply configuration action.</param>
        public void Supply(Action<FastlaneSupplyConfiguration> configurator)
        {
            if (configurator == null)
            {
                throw new ArgumentNullException(nameof(configurator));
            }

            var configuration = new FastlaneSupplyConfiguration();

            configurator(configuration);

            Supply(configuration);
        }

        /// <inheritdoc />
        ///  <example>
        ///      <code>
        ///          Fastlane.Update();
        ///      </code>
        ///  </example>
        public void Update()
        {
            if (_fastlaneToolProvider == null)
            {
                _fastlaneToolProvider = new FastlaneToolProvider(_context.FileSystem,
                    _context.Environment,
                    _context.ProcessRunner,
                    _context.Tools);
            }

            _fastlaneToolProvider.Update();
        }
    }
}